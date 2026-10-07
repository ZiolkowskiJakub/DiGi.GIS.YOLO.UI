using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.WebAPI.Classes;
using DiGi.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Writes the year built detection features of a run into the stored building data through the Web API.
        /// <para>The detections are turned into building data rows by <see cref="GIS.IO.Modify.Update_Building2D_YearBuiltPredictions"/> and posted to the building data update endpoint. Only the reference, the county and the detection columns travel, and the endpoint upserts on the columns it is given, so the rest of a building's row is left as it stands.</para>
        /// <para>This is where the detections are written from. The database side cannot do it: nothing in PostgreSQL stores a <see cref="Building2DYearBuiltPredictions"/>, so the run that produced them is the only thing that holds them (ZiolkowskiJakub/DiGi.GIS.PostgreSQL#57).</para>
        /// <para>A county is tens of thousands of buildings against ninety-odd detection columns, so the predictions are sent in batches rather than as one request.</para>
        /// <para>Naming the scored <paramref name="references"/> turns the write into a replacement of each scored building's detection columns over <paramref name="years"/>, so a building the current weights do not fire on loses what an earlier detector left on it (<see cref="Create.BuildingDataYearBuiltPredictionsTable"/>, ZiolkowskiJakub/DiGi.GIS.YOLO.UI#21). Without them only the detected buildings and years are written, as before.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyIds">The identifiers of the county rows the buildings belong to. Normally every polygon part of one county - the endpoint files each row under the part its reference belongs to.</param>
        /// <param name="building2DYearBuiltPredictions">The detections to write, one instance per building.</param>
        /// <param name="batchSize">The number of buildings sent in one request. Defaults to 5000.</param>
        /// <param name="postOptions">Optional configuration options for the POST request.</param>
        /// <param name="key">The optional API authorization key. Falls back to the key carried by <paramref name="postOptions"/> and then by the manager.</param>
        /// <param name="references">The references the detector scored, detected or not. Null writes only the detected buildings.</param>
        /// <param name="years">The year range whose detection columns are replaced when <paramref name="references"/> are named. Null applies the scoring step's default range.</param>
        /// <returns>A task returning <see langword="true"/> when every batch was accepted; otherwise <see langword="false"/>.</returns>
        public static async Task<bool> UpdateBuildingDataYearBuiltPredictionsAsync(this GISWebAPIManager? gisWebAPIManager, IEnumerable<int>? countyIds, IEnumerable<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions, int batchSize = 5000, PostOptions? postOptions = null, string? key = null, IEnumerable<string>? references = null, Range<int>? years = null)
        {
            if (gisWebAPIManager is null || countyIds is null || building2DYearBuiltPredictions is null)
            {
                return false;
            }

            List<int> countyIds_Valid = [.. countyIds.Where(x => x > 0).Distinct().OrderBy(x => x)];
            if (countyIds_Valid.Count == 0)
            {
                return false;
            }

            // The batches run over every reference written, detected or only scored, so a batch never carries a scored
            // building without the detections the same run found for it.
            Dictionary<string, Building2DYearBuiltPredictions> building2DYearBuiltPredictions_ByReference = new(StringComparer.Ordinal);
            foreach (Building2DYearBuiltPredictions building2DYearBuiltPredictions_Temp in building2DYearBuiltPredictions)
            {
                if (building2DYearBuiltPredictions_Temp?.Reference is string reference && !string.IsNullOrWhiteSpace(reference))
                {
                    building2DYearBuiltPredictions_ByReference[reference] = building2DYearBuiltPredictions_Temp;
                }
            }

            List<string> references_Write = [.. building2DYearBuiltPredictions_ByReference.Keys];
            if (references is not null)
            {
                HashSet<string> references_Unique = new(references_Write, StringComparer.Ordinal);
                foreach (string reference in references)
                {
                    if (!string.IsNullOrWhiteSpace(reference) && references_Unique.Add(reference))
                    {
                        references_Write.Add(reference);
                    }
                }
            }

            if (references_Write.Count == 0)
            {
                Serilog.Modify.Log("No year built predictions to write for counties {CountyIds}", string.Join(", ", countyIds_Valid));
                return true;
            }

            int size = batchSize < 1 ? 1 : batchSize;

            //The rows are stamped with the first part so the updater can build them; where a county has several parts the endpoint resolves each reference to its own part and restamps it
            int countyId_Stamp = countyIds_Valid[0];

            bool result = true;

            for (int i = 0; i < references_Write.Count; i += size)
            {
                int count = Math.Min(size, references_Write.Count - i);
                List<string> references_Batch = references_Write.GetRange(i, count);

                List<Building2DYearBuiltPredictions> building2DYearBuiltPredictions_Batch = [];
                foreach (string reference in references_Batch)
                {
                    if (building2DYearBuiltPredictions_ByReference.TryGetValue(reference, out Building2DYearBuiltPredictions? building2DYearBuiltPredictions_Temp) && building2DYearBuiltPredictions_Temp is not null)
                    {
                        building2DYearBuiltPredictions_Batch.Add(building2DYearBuiltPredictions_Temp);
                    }
                }

                Table? table = Create.BuildingDataYearBuiltPredictionsTable(countyId_Stamp, building2DYearBuiltPredictions_Batch, references is null ? null : references_Batch, years);
                if (table is null || table.RowCount == 0)
                {
                    continue;
                }

                bool updated = await WebAPI.Modify.UpdateItemsAsync(gisWebAPIManager, table, countyIds_Valid, postOptions, key);
                if (!updated)
                {
                    Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "Year built predictions batch was not stored - counties {CountyIds}, buildings {Start}..{End}", string.Join(", ", countyIds_Valid), i, i + count - 1);
                    result = false;
                    continue;
                }

                Serilog.Modify.Log("Year built predictions batch stored - counties {CountyIds}, buildings {Stored}/{Total}", string.Join(", ", countyIds_Valid), i + count, references_Write.Count);
            }

            return result;
        }

        /// <summary>
        /// Writes the year built detection features of a run into the stored building data through the Web API, for one explicitly identified county row.
        /// <para>Where a county is stored as several polygon parts, call the <see cref="IEnumerable{T}"/> overload with every part instead - naming one part files the whole batch there whether or not the buildings belong to it.</para>
        /// </summary>
        /// <param name="gisWebAPIManager">The <see cref="GISWebAPIManager"/> instance used to communicate with the WebAPI.</param>
        /// <param name="countyId">The identifier of the county row the buildings belong to.</param>
        /// <param name="building2DYearBuiltPredictions">The detections to write, one instance per building.</param>
        /// <param name="batchSize">The number of buildings sent in one request. Defaults to 5000.</param>
        /// <param name="postOptions">Optional configuration options for the POST request.</param>
        /// <param name="key">The optional API authorization key. Falls back to the key carried by <paramref name="postOptions"/> and then by the manager.</param>
        /// <param name="references">The references the detector scored, detected or not. Null writes only the detected buildings.</param>
        /// <param name="years">The year range whose detection columns are replaced when <paramref name="references"/> are named. Null applies the scoring step's default range.</param>
        /// <returns>A task returning <see langword="true"/> when every batch was accepted; otherwise <see langword="false"/>.</returns>
        public static async Task<bool> UpdateBuildingDataYearBuiltPredictionsAsync(this GISWebAPIManager? gisWebAPIManager, int countyId, IEnumerable<Building2DYearBuiltPredictions>? building2DYearBuiltPredictions, int batchSize = 5000, PostOptions? postOptions = null, string? key = null, IEnumerable<string>? references = null, Range<int>? years = null)
        {
            return await UpdateBuildingDataYearBuiltPredictionsAsync(gisWebAPIManager, [countyId], building2DYearBuiltPredictions, batchSize, postOptions, key, references, years);
        }
    }
}
