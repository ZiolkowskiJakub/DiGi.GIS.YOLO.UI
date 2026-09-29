using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.PostgreSQL.Table;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the training label of every labelled building of one county part: the stored <c>User year built</c> column of the building data, turned into one year per reference by <see cref="GIS.IO.Query.YearBuiltLabels(IEnumerable{Table?}?)"/>.
        /// <para>The same source and the same rule the Year Built regressor is trained on, so the detector and the regressor learn from identical labels: the most frequent <b>exact</b> user year, with bounded entries excluded. The column is written by the Year Built building data update, so that update has to have run since the last user edits for the labels to be current.</para>
        /// <para>The county is paged by reference: each page is the next <paramref name="pageSize"/> rows after the last reference of the previous one, and a short page - or a blank last reference, which would otherwise restart the county - ends it. Only the label column is projected; the server adds the reference and county columns on top.</para>
        /// <para><b>Any page that cannot be read fails the whole county</b> - the result is null, never a partial label set, because a builder handed half a county&apos;s labels would build a dataset that silently misses the other half.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county part to read.</param>
        /// <param name="pageSize">The number of rows read in one request, clamped to [1, <see cref="Constants.Count.BuildingDataReference_Maximum"/>].</param>
        /// <param name="postOptions">Optional configuration options for the requests.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task returning the label year by reference - an unlabelled building is absent - or null when any page could not be read.</returns>
        public static async Task<Dictionary<string, short>?> UserYearBuiltsAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, int pageSize = Constants.Count.BuildingDataReference_Maximum, PostOptions? postOptions = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null || countyId <= 0)
            {
                return null;
            }

            if (GIS.IO.Constants.Column.UserYearBuilt.UniqueId() is not string columnUniqueId_UserYearBuilt)
            {
                return null;
            }

            HttpClient? httpClient = gisWebAPIManager.CreateHttpClient<BuildingDataController>(nameof(BuildingDataController.GetTableByBuildingDataByPagingParameterAsync), out string? path);
            if (httpClient is null || string.IsNullOrWhiteSpace(path))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for {Method} could not be resolved", nameof(BuildingDataController.GetTableByBuildingDataByPagingParameterAsync));
                return null;
            }

            pageSize = pageSize < 1 ? 1 : Math.Min(pageSize, Constants.Count.BuildingDataReference_Maximum);

            // A page of a county is sized against the server's command timeout, not the twenty second item default.
            PostOptions postOptions_Temp = postOptions ?? new PostOptions() { RequestResult = true, Delay = TimeSpan.FromSeconds(60) };

            List<Table?> tables = [];
            string? cursor = null;
            int rowCount_Page;

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Built member by member from the parameter type rather than serialized from an instance of it, so a
                // member renamed on the server side stops compiling here instead of quietly binding to nothing.
                JsonObject jsonObject = new()
                {
                    [nameof(BuildingDataByPagingParameter.CountyId)] = countyId,
                    [nameof(BuildingDataByPagingParameter.ColumnUniqueIds)] = new JsonArray(columnUniqueId_UserYearBuilt),
                    [nameof(BuildingDataByPagingParameter.PageSize)] = pageSize
                };

                if (!string.IsNullOrWhiteSpace(cursor))
                {
                    jsonObject[nameof(BuildingDataByPagingParameter.Cursor)] = cursor;
                }

                string json_Request = jsonObject.ToJsonString();

                Table? table;
                try
                {
                    // Passed as a factory, so a retry rebuilds the body rather than resending a drained stream.
                    PostResponse<string?> postResponse = await DiGi.WebAPI.Modify.PostAsync<string>(httpClient, path, () => GIS.WebAPI.Create.HttpContent(json_Request, cancellationToken), postOptions_Temp);

                    string? json = postResponse is not null && postResponse.Succeeded ? postResponse.Result : null;

                    table = string.IsNullOrWhiteSpace(json) ? null : GIS.WebAPI.Create.Table(JsonNode.Parse(json!) as JsonObject);
                }
                catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                {
                    Serilog.Modify.Log(exception, "A page of the user year built column could not be read for county {CountyId} after {Cursor}", countyId, cursor ?? "(start)");
                    table = null;
                }

                if (table is null)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "The user year built labels of county {CountyId} could not be read in full - the county is refused rather than labelled in part", countyId);
                    return null;
                }

                tables.Add(table);

                rowCount_Page = table.RowCount;

                int index_Reference = table.GetColumnIndex(GIS.IO.Constants.Column.Reference.Name);
                string? reference_Last = rowCount_Page == 0 || index_Reference < 0 ? null : table.GetValue<string>(rowCount_Page - 1, index_Reference);

                // A blank last reference ends the county rather than becoming the next cursor: a blank cursor is
                // dropped from the request, which would return the first page again and never end.
                cursor = string.IsNullOrWhiteSpace(reference_Last) ? null : reference_Last;
            }
            while (rowCount_Page == pageSize && cursor is not null);

            return GIS.IO.Query.YearBuiltLabels(tables);
        }
    }
}
