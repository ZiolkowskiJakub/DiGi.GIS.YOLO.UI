using DiGi.GIS.Classes;
using DiGi.GIS.PostgreSQL.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Exports orthophoto prediction images from the database for a specified county to the designated output directory.
        /// <para>Decodes binary payloads from <see cref="OrtoData.Bytes"/> and re-encodes them as JPEG files named <c>{reference}_{year}.jpeg</c>.</para>
        /// <para>Named <paramref name="references"/> narrow the county to those buildings, so a run that only needs the labelled buildings of a training dataset does not fetch a whole county of imagery for them.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The integer identifier of the county partition to export images for.</param>
        /// <param name="destinationDirectory">The target directory path on disk where JPEG files will be saved.</param>
        /// <param name="maxConcurrentRequests">The maximum number of concurrent WebAPI requests allowed during image fetching. Defaults to 8.</param>
        /// <param name="resume">When <see langword="true"/>, skips downloading or re-encoding images already present on disk. Defaults to <see langword="true"/>.</param>
        /// <param name="references">The only buildings to export, intersected with the county's orthophoto listing - a reference the county does not hold is ignored. Null exports every building of the county (ZiolkowskiJakub/DiGi.GIS.YOLO.UI#22).</param>
        /// <param name="cancellationToken">A cancellation token to observe while performing the operation.</param>
        /// <returns>A task returning <see langword="true"/> if the export completed successfully; otherwise <see langword="false"/>.</returns>
        [SupportedOSPlatform("windows")]
        public static async Task<bool> ExportPredictionImagesAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, string? destinationDirectory, int maxConcurrentRequests = 8, bool resume = true, IEnumerable<string>? references = null, CancellationToken cancellationToken = default)
        {
            if (gisWebAPIManager is null || countyId <= 0 || string.IsNullOrWhiteSpace(destinationDirectory))
            {
                return false;
            }

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            HttpClient? httpClient_References = gisWebAPIManager.CreateHttpClient<OrtoDatasController>(nameof(OrtoDatasController.GetOrtoDatasReferencesByCountyIdAsync), out string? path_References);
            if (httpClient_References is null || string.IsNullOrWhiteSpace(path_References))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for GetOrtoDatasReferencesByCountyIdAsync could not be resolved");
                return false;
            }

            HttpClient? httpClient_Item = gisWebAPIManager.CreateHttpClient<OrtoDatasController>(nameof(OrtoDatasController.GetItemByReferenceAsync), out string? path_Item);
            if (httpClient_Item is null || string.IsNullOrWhiteSpace(path_Item))
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "HttpClient or path for GetItemByReferenceAsync could not be resolved");
                return false;
            }

            string requestUri_References = new UrlBuilder(path_References).AddParameter("countyid", countyId).ToString();
            PostOptions postOptions = new() { RequestResult = true };

            PostResponse<List<OrtoDatasReference>?> postResponse_References;
            try
            {
                postResponse_References = await DiGi.WebAPI.Query.GetAsync<List<OrtoDatasReference>>(httpClient_References, requestUri_References, postOptions);
            }
            catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
            {
                Serilog.Modify.Log(exception, "Failed to retrieve OrtoDatasReference list for county {CountyId}", countyId);
                return false;
            }

            if (postResponse_References is null || !postResponse_References.Succeeded || postResponse_References.Result is not List<OrtoDatasReference> ortoDatasReferences)
            {
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "OrtoDatasReferences query returned unsuccessful result for county {CountyId}", countyId);
                return false;
            }

            if (ortoDatasReferences.Count == 0)
            {
                // Not a failure - a county genuinely may hold no imagery yet - but it is the shape a mis-scoped run
                // takes as well, and everything downstream of here then reports a legitimate zero. Warning rather
                // than Information so an unattended run leaves a trace of why it scored nothing.
                Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Warning, "No OrtoDatasReferences found for county {CountyId} - nothing will be exported, detected or scored for it", countyId);
                return true;
            }

            HashSet<string>? references_Filter = references is null ? null : new(references.Where(x => !string.IsNullOrWhiteSpace(x)), StringComparer.Ordinal);

            List<OrtoDatasReference> ortoDatasReferences_Valid = [];
            foreach (OrtoDatasReference ortoDatasReference in ortoDatasReferences)
            {
                if (ortoDatasReference is null || string.IsNullOrWhiteSpace(ortoDatasReference.Reference))
                {
                    continue;
                }

                if (references_Filter is not null && !references_Filter.Contains(ortoDatasReference.Reference!))
                {
                    continue;
                }

                if (resume && Directory.EnumerateFiles(destinationDirectory, $"{ortoDatasReference.Reference}_*.jpeg").Any())
                {
                    continue;
                }

                ortoDatasReferences_Valid.Add(ortoDatasReference);
            }

            if (ortoDatasReferences_Valid.Count == 0)
            {
                Serilog.Modify.Log("All prediction images already exist on disk for county {CountyId}", countyId);
                return true;
            }

            int concurrencyLimit = maxConcurrentRequests < 1 ? 1 : maxConcurrentRequests;

            for (int i = 0; i < ortoDatasReferences_Valid.Count; i += concurrencyLimit)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int batchSize = Math.Min(concurrencyLimit, ortoDatasReferences_Valid.Count - i);
                List<Task> tasks = [];

                for (int j = 0; j < batchSize; j++)
                {
                    int index = i + j;
                    string reference = ortoDatasReferences_Valid[index].Reference!;

                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            string requestUri_Item = new UrlBuilder(path_Item).AddParameter("reference", reference).AddParameter("countyid", countyId).ToString();

                            PostResponse<DiGi.GIS.Classes.OrtoDatas?> postResponse_Item = await DiGi.WebAPI.Query.GetAsync<DiGi.GIS.Classes.OrtoDatas>(httpClient_Item, requestUri_Item, postOptions);
                            if (postResponse_Item is null || !postResponse_Item.Succeeded || postResponse_Item.Result is not DiGi.GIS.Classes.OrtoDatas ortoDatas)
                            {
                                return;
                            }

                            if (string.IsNullOrWhiteSpace(ortoDatas.Reference))
                            {
                                return;
                            }

                            foreach (OrtoData ortoData in ortoDatas)
                            {
                                if (ortoData?.Bytes is null || ortoData.Bytes.Length == 0)
                                {
                                    continue;
                                }

                                string fileName = $"{ortoDatas.Reference}_{ortoData.DateTime.Year}.jpeg";
                                string filePath = Path.Combine(destinationDirectory, fileName);

                                if (resume && File.Exists(filePath))
                                {
                                    continue;
                                }

                                ortoData.SavePredictionImage(filePath, out _, out _);
                            }
                        }
                        catch (Exception exception) when (!cancellationToken.IsCancellationRequested)
                        {
                            Serilog.Modify.Log(exception, "Failed to export prediction image for reference {Reference} in county {CountyId}", reference, countyId);
                        }
                    }, cancellationToken));
                }

                await Task.WhenAll(tasks);
            }

            return true;
        }
    }
}
