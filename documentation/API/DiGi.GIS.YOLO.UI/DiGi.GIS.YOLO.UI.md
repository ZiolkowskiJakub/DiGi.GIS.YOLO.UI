#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken)'></a>

## Modify\.ExportPredictionImagesAsync\(this GISWebAPIManager, int, string, int, bool, CancellationToken\) Method

Exports orthophoto prediction images from the database for a specified county to the designated output directory\.

Decodes binary payloads from [DiGi\.GIS\.Classes\.OrtoData\.Bytes](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata.bytes 'DiGi\.GIS\.Classes\.OrtoData\.Bytes') and re-encodes them as JPEG files named `{reference}_{year}.jpeg`.

```csharp
public static System.Threading.Tasks.Task<bool> ExportPredictionImagesAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, string? destinationDirectory, int maxConcurrentRequests=8, bool resume=true, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The integer identifier of the county partition to export images for\.

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).destinationDirectory'></a>

`destinationDirectory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The target directory path on disk where JPEG files will be saved\.

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).maxConcurrentRequests'></a>

`maxConcurrentRequests` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The maximum number of concurrent WebAPI requests allowed during image fetching\. Defaults to 8\.

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).resume'></a>

`resume` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool'), skips downloading or re\-encoding images already present on disk\. Defaults to [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

A cancellation token to observe while performing the operation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the export completed successfully; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## Modify\.RunYearBuiltPredictionsAsync\(this GISWebAPIManager, IYearBuiltPredictor, YearBuiltPredictionPipelineOptions, IProgress\<long\>, CancellationToken\) Method

Runs the Year Built prediction pipeline over the counties named in the options, from the stored orthophoto imagery through to the stored prediction\.

Six steps per county: export the imagery, score it with the frozen detector, turn the detections into objects, write them into the building data, read the feature columns back and score them into a construction year, and store that year twice - dated into the year built data, and latest into the building data column.

Each step carries its own flag, so a run can be resumed without repeating the expensive ones, and a first pass over a county can be made harmless by turning the three write steps off. Each step is idempotent: the scratch paths are derived from the county identifier, the detector overwrites its results file rather than appending to it, and a stored year built datum is read back and added to rather than replaced.

Only a building the detector fired on at least once is scored. A building it never fired on carries no per-year confidence series, which is the feature the regressor was built around, so scoring it would be scoring a row of absent features. The consequence is that the run predicts a year for fewer buildings than the file based workflow it replaces, which scored every row of its table - worth knowing before comparing the two reference by reference.

The scope is checked before any of it starts. A county identifier that is in no county row - most often a four character county code passed where an identifier was wanted - matches no stored building, so every step reports a legitimate zero and the run ends green having done nothing at all. That is a mis-scoped run rather than an empty county, so it fails here instead.

A county that fails is logged and stepped over, so one unreachable county cannot cost the run the counties behind it. The result therefore comes back either way - [FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult\.FailedStepNames') is what says whether the run did everything it set out to do.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult?> RunYearBuiltPredictionsAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, DiGi.GIS.IO.Interfaces.IYearBuiltPredictor? yearBuiltPredictor, DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions? yearBuiltPredictionPipelineOptions=null, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\. It also carries the key the write steps authorize with\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken).yearBuiltPredictor'></a>

`yearBuiltPredictor` [DiGi\.GIS\.IO\.Interfaces\.IYearBuiltPredictor](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.interfaces.iyearbuiltpredictor 'DiGi\.GIS\.IO\.Interfaces\.IYearBuiltPredictor')

The regressor that turns building features into a construction year\. Required only when the options ask for the scoring step\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken).yearBuiltPredictionPipelineOptions'></a>

`yearBuiltPredictionPipelineOptions` [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions')

The options describing the run\. Null uses the defaults, which name no county and therefore do nothing\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

An optional progress reporter carrying the running total of buildings the run has carried through a step\. A building is counted once per step it clears\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.IO.Interfaces.IYearBuiltPredictor,DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning what the run did, or null when the run could not be attempted at all \- no manager, no county named, or no scratch directory\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string)'></a>

## Modify\.UpdateBuildingDataYearBuiltPredictionsAsync\(this GISWebAPIManager, int, IEnumerable\<Building2DYearBuiltPredictions\>, int, PostOptions, string\) Method

Writes the year built detection features of a run into the stored building data through the Web API, for one explicitly identified county row\.

Where a county is stored as several polygon parts, call the [System\.Collections\.Generic\.IEnumerable&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1') overload with every part instead - naming one part files the whole batch there whether or not the buildings belong to it.

```csharp
public static System.Threading.Tasks.Task<bool> UpdateBuildingDataYearBuiltPredictionsAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2DYearBuiltPredictions>? building2DYearBuiltPredictions, int batchSize=5000, DiGi.WebAPI.Classes.PostOptions? postOptions=null, string? key=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row the buildings belong to\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).building2DYearBuiltPredictions'></a>

`building2DYearBuiltPredictions` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2dyearbuiltpredictions 'DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The detections to write, one instance per building\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).batchSize'></a>

`batchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of buildings sent in one request\. Defaults to 5000\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the POST request\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).key'></a>

`key` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional API authorization key\. Falls back to the key carried by [postOptions](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).postOptions 'DiGi\.GIS\.YOLO\.UI\.Modify\.UpdateBuildingDataYearBuiltPredictionsAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\>, int, DiGi\.WebAPI\.Classes\.PostOptions, string\)\.postOptions') and then by the manager\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when every batch was accepted; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string)'></a>

## Modify\.UpdateBuildingDataYearBuiltPredictionsAsync\(this GISWebAPIManager, IEnumerable\<int\>, IEnumerable\<Building2DYearBuiltPredictions\>, int, PostOptions, string\) Method

Writes the year built detection features of a run into the stored building data through the Web API\.

The detections are turned into building data rows by [DiGi\.GIS\.IO\.Modify\.Update\_Building2D\_YearBuiltPredictions\(DiGi\.Core\.IO\.Table\.Classes\.Table,System\.Int32,System\.Collections\.Generic\.IEnumerable\{DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\}\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.modify.update_building2d_yearbuiltpredictions#digi-gis-io-modify-update_building2d_yearbuiltpredictions(digi-core-io-table-classes-table-system-int32-system-collections-generic-ienumerable{digi-gis-classes-building2dyearbuiltpredictions}) 'DiGi\.GIS\.IO\.Modify\.Update\_Building2D\_YearBuiltPredictions\(DiGi\.Core\.IO\.Table\.Classes\.Table,System\.Int32,System\.Collections\.Generic\.IEnumerable\{DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\}\)') and posted to the building data update endpoint. Only the reference, the county and the detection columns travel, and the endpoint upserts on the columns it is given, so the rest of a building's row is left as it stands.

This is where the detections are written from. The database side cannot do it: nothing in PostgreSQL stores a [DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2dyearbuiltpredictions 'DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions'), so the run that produced them is the only thing that holds them (ZiolkowskiJakub/DiGi.GIS.PostgreSQL#57).

A county is tens of thousands of buildings against ninety-odd detection columns, so the predictions are sent in batches rather than as one request.

```csharp
public static System.Threading.Tasks.Task<bool> UpdateBuildingDataYearBuiltPredictionsAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, System.Collections.Generic.IEnumerable<int>? countyIds, System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2DYearBuiltPredictions>? building2DYearBuiltPredictions, int batchSize=5000, DiGi.WebAPI.Classes.PostOptions? postOptions=null, string? key=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The identifiers of the county rows the buildings belong to\. Normally every polygon part of one county \- the endpoint files each row under the part its reference belongs to\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).building2DYearBuiltPredictions'></a>

`building2DYearBuiltPredictions` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2dyearbuiltpredictions 'DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The detections to write, one instance per building\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).batchSize'></a>

`batchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of buildings sent in one request\. Defaults to 5000\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the POST request\.

<a name='DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).key'></a>

`key` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional API authorization key\. Falls back to the key carried by [postOptions](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.UpdateBuildingDataYearBuiltPredictionsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,System.Collections.Generic.IEnumerable_int_,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2DYearBuiltPredictions_,int,DiGi.WebAPI.Classes.PostOptions,string).postOptions 'DiGi\.GIS\.YOLO\.UI\.Modify\.UpdateBuildingDataYearBuiltPredictionsAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, System\.Collections\.Generic\.IEnumerable\<int\>, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\>, int, DiGi\.WebAPI\.Classes\.PostOptions, string\)\.postOptions') and then by the manager\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') when every batch was accepted; otherwise [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.YOLO.UI.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.BuildingDataTableAsync\(this GISWebAPIManager, int, IEnumerable\<string\>, IEnumerable\<string\>, PostOptions, CancellationToken\) Method

Reads the stored building data of the named references as a table, projected to the named columns\.

The projection is an allow-list rather than a filter. Asking for every column would hand a regressor the pipeline's own output column back as an input feature, which reads as a large accuracy gain rather than as a defect.

The endpoint refuses more than [BuildingDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.BuildingDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.BuildingDataReference\_Maximum') references in one request and a county is far larger than that, so the caller pages the references rather than this method doing it - a page is the unit that succeeds or fails.

```csharp
public static System.Threading.Tasks.Task<DiGi.Core.IO.Table.Classes.Table?> BuildingDataTableAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IEnumerable<string>? references, System.Collections.Generic.IEnumerable<string>? columnUniqueIds, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row the references belong to\.

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The building references to read, at most the endpoint's cap\.

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).columnUniqueIds'></a>

`columnUniqueIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The unique identifiers of the columns to project\. Null or empty asks for every column, which this pipeline never wants\.

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the request\.

<a name='DiGi.GIS.YOLO.UI.Query.BuildingDataTableAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the projected table, or null when it could not be read\.

<a name='DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string)'></a>

## Query\.ConfigurationFilePath\(string\) Method

Resolves where a deployed configuration file of the given name sits\.

Both copy targets flatten into the output root - `CopyUserFiles` runs after `CopyFiles`, so a secret in the git-ignored `user files` folder overwrites the committed default of the same name - which is why only the output root is probed. A `bin\user files` folder is never produced, so looking for one would read as a working fallback while finding nothing.

```csharp
public static string? ConfigurationFilePath(string? fileName);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string).fileName'></a>

`fileName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name of the configuration file, without a directory\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The full path the file would have, whether or not it exists, or null when neither directory can be resolved or no name was given\.

<a name='DiGi.GIS.YOLO.UI.Query.CountyReferencesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.WebAPI.Classes.PostOptions)'></a>

## Query\.CountyReferencesAsync\(this GISWebAPIManager, PostOptions\) Method

Reads every stored county row\.

One row per polygon part rather than one per county, so a county whose territory is in several pieces appears several times under one [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Code](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference.code 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Code'). That is what makes this the answer to both questions the run asks of it: whether a named identifier is a county row at all, and which sibling parts it has.

One request answers both for the whole run, so it is made once and the answer reused.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>?> CountyReferencesAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, DiGi.WebAPI.Classes.PostOptions? postOptions=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.CountyReferencesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.WebAPI.Classes.PostOptions).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.CountyReferencesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.WebAPI.Classes.PostOptions).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the request\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the county rows, or null when they could not be read\. Null and an empty list mean different things: the first is a failed read, the second a stored estate with no counties in it\.

<a name='DiGi.GIS.YOLO.UI.Query.Key(string)'></a>

## Query\.Key\(string\) Method

Reads the API authorization key from the default or specified configuration file path\.

```csharp
public static string? Key(string? path=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.Key(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional file path to GIS\_WebAPI\_Client\.conf\. If omitted, [ConfigurationFilePath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ConfigurationFilePath\(string\)') resolves it against the deployed output\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The API key if found; otherwise, null\.

<a name='DiGi.GIS.YOLO.UI.Query.ModelPath(string)'></a>

## Query\.ModelPath\(string\) Method

Resolves the absolute path to the YOLO model file from the specified path or standard deployment locations\.

```csharp
public static string? ModelPath(string? modelPath);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.ModelPath(string).modelPath'></a>

`modelPath` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The configured model path, which may be relative to the application directory or user files directory\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The resolved absolute path if the model file exists; otherwise, the normalized path or null\.

<a name='DiGi.GIS.YOLO.UI.Query.SiblingCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_)'></a>

## Query\.SiblingCountyIds\(IEnumerable\<AdministrativeAreal2DReference\>, IEnumerable\<int\>\) Method

Resolves, for each county row named, every polygon part of the county that row belongs to\.

A county whose territory is in several pieces is held as one row per piece, so a county identifier names a part rather than a county. The write endpoints file each item under the part its reference belongs to, and can only do that when they are told which parts are in play - naming one part of a multi-part county files the whole batch there whether or not the buildings belong to it.

A county row the list does not cover is left out rather than guessed at. Ask [UnknownCountyIds\(IEnumerable&lt;AdministrativeAreal2DReference&gt;, IEnumerable&lt;int&gt;\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.UnknownCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_) 'DiGi\.GIS\.YOLO\.UI\.Query\.UnknownCountyIds\(System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\>, System\.Collections\.Generic\.IEnumerable\<int\>\)') about those before running anything: an identifier that is in no county row is a mis-scoped run, not a county with one part.

```csharp
public static System.Collections.Generic.Dictionary<int,System.Collections.Generic.List<int>> SiblingCountyIds(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences, System.Collections.Generic.IEnumerable<int>? countyIds);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.SiblingCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_).administrativeAreal2DReferences'></a>

`administrativeAreal2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The stored county rows, as read by [CountyReferencesAsync\(this GISWebAPIManager, PostOptions\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.CountyReferencesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.WebAPI.Classes.PostOptions) 'DiGi\.GIS\.YOLO\.UI\.Query\.CountyReferencesAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, DiGi\.WebAPI\.Classes\.PostOptions\)')\.

<a name='DiGi.GIS.YOLO.UI.Query.SiblingCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The county rows to resolve\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
Each named county row mapped to the polygon parts of its county, ordered ascending\.

<a name='DiGi.GIS.YOLO.UI.Query.UnknownCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_)'></a>

## Query\.UnknownCountyIds\(IEnumerable\<AdministrativeAreal2DReference\>, IEnumerable\<int\>\) Method

Picks out the named county identifiers that are not county rows, and works out whether each was meant as a county code\.

A county is addressed by [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Id](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference.id 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Id'), which is a database identifier running into six figures. [DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Code](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference.code 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference\.Code') is the four character territorial code, and the two are easy to confuse because a code reads as a number: asking for county 2212 asks for an identifier that does not exist, while the code 2212 is a real county held as two polygon parts under quite different identifiers.

Nothing downstream can tell the difference on its own. An identifier in no county row simply matches no stored building, so the run exports no imagery, detects nothing, scores nothing and reports every one of those as a legitimate zero. That is why the scope is checked here, before any of it starts.

```csharp
public static System.Collections.Generic.Dictionary<int,System.Collections.Generic.List<int>> UnknownCountyIds(System.Collections.Generic.IEnumerable<DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference>? administrativeAreal2DReferences, System.Collections.Generic.IEnumerable<int>? countyIds);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.UnknownCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_).administrativeAreal2DReferences'></a>

`administrativeAreal2DReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.administrativeareal2dreference 'DiGi\.GIS\.PostgreSQL\.Classes\.AdministrativeAreal2DReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The stored county rows, as read by [CountyReferencesAsync\(this GISWebAPIManager, PostOptions\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.CountyReferencesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.WebAPI.Classes.PostOptions) 'DiGi\.GIS\.YOLO\.UI\.Query\.CountyReferencesAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, DiGi\.WebAPI\.Classes\.PostOptions\)')\.

<a name='DiGi.GIS.YOLO.UI.Query.UnknownCountyIds(System.Collections.Generic.IEnumerable_DiGi.GIS.PostgreSQL.Classes.AdministrativeAreal2DReference_,System.Collections.Generic.IEnumerable_int_).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The county identifiers the run was scoped to\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
Each named identifier that is not a county row, mapped to the identifiers of the county whose code it spells, ordered ascending\. The mapped list is empty when the value is not a county code either, and the whole dictionary is empty when every named identifier is a county row\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.YearBuiltDatasAsync\(this GISWebAPIManager, int, IDictionary\<string,short\>, DateTimeOffset, bool, int, PostOptions, CancellationToken\) Method

Reads each building's stored year built data and adds the run's predicted construction year to it\.

The stored entry is read back rather than a fresh one built, because the year built table addresses a stored object by its own identifier: a datum built fresh carries a new one and is stored <i>alongside</i> whatever the building already holds instead of replacing it. Reading it back is also what preserves the history and any user-supplied year.

A building with nothing stored yet gets a new datum, which is the one case where a fresh identifier is correct.

Every prediction of one run carries the same stamp. The stored entries are keyed by it, so one stamp per run leaves one history entry per run, and re-running with the same stamp replaces that entry rather than adding to it.

There is no bulk read for this table - the endpoint answers one reference at a time - so the reads are issued in bounded batches rather than all at once.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.Classes.YearBuiltData>> YearBuiltDatasAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IDictionary<string,short>? years, System.DateTimeOffset runTimestamp, bool readStored=true, int maxConcurrentRequests=8, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row the references belong to\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).years'></a>

`years` [System\.Collections\.Generic\.IDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')

The predicted construction year of each building, by reference\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).runTimestamp'></a>

`runTimestamp` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The stamp every prediction of this run carries\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).readStored'></a>

`readStored` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When true, each building's stored entry is read back first so the prediction is added to its history\. Set it false only when the caller is not storing the year built data at all \- a county is tens of thousands of buildings and this is a request each, while the building data column is derived from the latest prediction, which a fresh entry already carries\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).maxConcurrentRequests'></a>

`maxConcurrentRequests` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The maximum number of concurrent WebAPI requests allowed while reading\. Defaults to 8\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the requests\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.GIS\.Classes\.YearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.yearbuiltdata 'DiGi\.GIS\.Classes\.YearBuiltData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the year built data to store, each carrying its building's history plus this run's prediction\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltPredictionPipelineOptions(string)'></a>

## Query\.YearBuiltPredictionPipelineOptions\(string\) Method

Reads and deserializes the [YearBuiltPredictionPipelineOptions\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YearBuiltPredictionPipelineOptions(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.YearBuiltPredictionPipelineOptions\(string\)') from the specified path or default locations\.

A member the file does not name keeps the class default, and a key the class does not declare is dropped in silence - so a misspelt flag reads as an unchanged one. The committed template beside the deployed application is the authority on the spelling.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions? YearBuiltPredictionPipelineOptions(string? path=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltPredictionPipelineOptions(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional file path to YearBuiltPredictionPipelineOptions\.json\. If omitted, [ConfigurationFilePath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ConfigurationFilePath\(string\)') resolves it against the deployed output\.

#### Returns
[YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions')  
The deserialized options instance, or null if not found or invalid\.