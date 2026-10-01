#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.GIS.YOLO.UI.Convert.ToDiGi_DatasetReference(thisstring)'></a>

## Convert\.ToDiGi\_DatasetReference\(this string\) Method

Parses one row of the `dataset_references.tsv` manifest, written by [ToTSV\(this DatasetReference\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Convert.ToTSV(thisDiGi.GIS.YOLO.UI.Classes.DatasetReference) 'DiGi\.GIS\.YOLO\.UI\.Convert\.ToTSV\(this DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference\)'), back into a dataset building\.

`Legacy` is not read: it is derived from `LegacySource`, so the two cannot disagree.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.DatasetReference? ToDiGi_DatasetReference(this string? line);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Convert.ToDiGi_DatasetReference(thisstring).line'></a>

`line` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The tab\-separated row, in the column order of [DatasetReferences](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Header.DatasetReferences 'DiGi\.GIS\.YOLO\.UI\.Constants\.Header\.DatasetReferences')\.

#### Returns
[DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference')  
The dataset building, or null when the row is blank, is the header, or does not parse\.

<a name='DiGi.GIS.YOLO.UI.Convert.ToTSV(thisDiGi.GIS.YOLO.UI.Classes.DatasetReference)'></a>

## Convert\.ToTSV\(this DatasetReference\) Method

Formats a dataset building as one row of the `dataset_references.tsv` manifest, in the column order of [DatasetReferences](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Header.DatasetReferences 'DiGi\.GIS\.YOLO\.UI\.Constants\.Header\.DatasetReferences')\.

Numbers are written with [System\.Globalization\.CultureInfo\.InvariantCulture](https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.invariantculture 'System\.Globalization\.CultureInfo\.InvariantCulture'), the split by its enum name, `Legacy` as `true` / `false`, and `LegacySource` by its description - empty for a clean building.

```csharp
public static string? ToTSV(this DiGi.GIS.YOLO.UI.Classes.DatasetReference? datasetReference);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Convert.ToTSV(thisDiGi.GIS.YOLO.UI.Classes.DatasetReference).datasetReference'></a>

`datasetReference` [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference')

The dataset building\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The tab\-separated row, without a line break, or null when there is no building or no reference\.

<a name='DiGi.GIS.YOLO.UI.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.GIS.YOLO.UI.Create.ProgressMessage(long)'></a>

## Create\.ProgressMessage\(long\) Method

Builds the line the headless runner writes to report how far a run has got\.

The line is read back by [ProgressCount\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ProgressCount(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ProgressCount\(string\)'), so both sides of the pipe are built from this one method rather than from a format literal written twice. A caller watching the runner's standard output has no other way to learn what a long run is doing.

Invariant culture, because the reader is a machine: a thousands separator taken from the machine's own settings would make the count unparseable on exactly the machines that use one.

```csharp
public static string ProgressMessage(long count);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Create.ProgressMessage(long).count'></a>

`count` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The running total of items the run has carried through a step\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The progress line, without a trailing line break\.

<a name='DiGi.GIS.YOLO.UI.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.GIS.YOLO.UI.Modify.Add(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount)'></a>

## Modify\.Add\(this YOLOTrainingDatasetCount, YOLOTrainingDatasetCount\) Method

Adds every tally of one YOLO training dataset count to another \- a county part's into the run total, or one building's into its county part's\.

The county identifier of the target is left as it is.

```csharp
public static bool Add(this DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount? yOLOTrainingDatasetCount, DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount? yOLOTrainingDatasetCount_Add);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.Add(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount).yOLOTrainingDatasetCount'></a>

`yOLOTrainingDatasetCount` [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')

The count added to\.

<a name='DiGi.GIS.YOLO.UI.Modify.Add(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount).yOLOTrainingDatasetCount_Add'></a>

`yOLOTrainingDatasetCount_Add` [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')

The count added\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True when both counts were given and the tallies were added\.

<a name='DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken)'></a>

## Modify\.AppendYOLOTrainingDatasetAsync\(this GISWebAPIManager, YOLOTrainingDatasetOptions, IProgress\<long\>, CancellationToken\) Method

Builds a YOLO training dataset of every labelled building of the named county parts from the deployed data: labels from the building data, footprints and orthophotos from their tables, read through the Web API\.

<b>Labels.</b> The stored `User year built` column ([UserYearBuiltsAsync\(this GISWebAPIManager, int, int, PostOptions, CancellationToken\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken) 'DiGi\.GIS\.YOLO\.UI\.Query\.UserYearBuiltsAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, int, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)')) - the same exact user year the Year Built regressor trains on. A building without one is not in the dataset. A reference filed under several named county parts is built once, under the lowest identifier.

<b>Split.</b> A reference [DiGi\.GIS\.IO\.Query\.Holdout\(System\.String,System\.Int32\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.query.holdout#digi-gis-io-query-holdout(system-string-system-int32) 'DiGi\.GIS\.IO\.Query\.Holdout\(System\.String,System\.Int32\)') holds out goes to Test and only there - the same buildings the regressor holds out. The rest, sorted ordinally, are split by a seeded draw into Validate ([ValidateWeight](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ValidateWeight 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.ValidateWeight')) and Train, so the same labels and seed give the same split.

<b>Images.</b> One per orthophoto year - the first of a year in date order - saved by [SavePredictionImage\(this OrtoData, string, int, int\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int) 'DiGi\.GIS\.YOLO\.UI\.Modify\.SavePredictionImage\(this DiGi\.GIS\.Classes\.OrtoData, string, int, int\)'), the encoder the inference export uses, as `{reference}_{year}.jpeg`. A year at or after the label carries one box of class `Building` (index 0): the footprint's bounding box grown by [Offset](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Offset 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.Offset'), projected onto the image and clamped to it ([PixelBoundingBox\(this OrtoData, BoundingBox2D, double, int, int, bool\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool) 'DiGi\.GIS\.YOLO\.UI\.Query\.PixelBoundingBox\(this DiGi\.GIS\.Classes\.OrtoData, DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D, double, int, int, bool\)')). A year before the label is kept with an empty label file - it teaches the boundary. A positive year whose box has no area inside the image is not written at all. Identical photo bytes under two years are kept once when their labels agree and dropped when they conflict.

<b>Legacy.</b> Each Test building is checked against the two records of what the lost `train8` dataset held ([LegacySource\(ISet&lt;string&gt;, string, IEnumerable&lt;YearBuiltData&gt;, DateTimeOffset\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset) 'DiGi\.GIS\.YOLO\.UI\.Query\.LegacySource\(System\.Collections\.Generic\.ISet\<string\>, string, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Classes\.YearBuiltData\>, System\.DateTimeOffset\)')), so the detector evaluation can report a subset `train8` cannot have seen. A county part whose stored history cannot be read in full has the decision refused for all of its Test buildings - they are [Unknown](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.Unknown 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.Unknown'), never clean.

<b>Resume.</b> The `dataset_references.tsv` manifest is a journal: a building's images and label files are written first and its row appended after, so a building the manifest names is complete and is skipped, and the files of one it does not name - left half-written by a stopped run - are removed and rebuilt. `conf.yaml` is written before the first building and again at the end.

<b>CountOnly</b> stops before any orthophoto is requested and reports, per county part, the labelled buildings, the split, the Legacy agreement table and bounded entries of the Test buildings, the cross-part reference duplicates and an estimate of a build's size.

A building that fails is logged and stepped over; a county part whose labels cannot be read is refused while the others are built. [FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult\.FailedStepNames') is what says whether the run did everything it set out to do.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult?> AppendYOLOTrainingDatasetAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, System.IProgress<long>? progress=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken).yOLOTrainingDatasetOptions'></a>

`yOLOTrainingDatasetOptions` [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions')

The options describing the dataset\.

<a name='DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

An optional progress reporter carrying the running total of buildings the run has carried through\.

<a name='DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning what the run did, or null when it could not be attempted at all \- no manager, no options, no county named, or no absolute output directory\.

<a name='DiGi.GIS.YOLO.UI.Modify.CheckYOLOTrainingDatasetLabels(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken)'></a>

## Modify\.CheckYOLOTrainingDatasetLabels\(this YOLOTrainingDatasetOptions, CancellationToken\) Method

Checks the label boxes of a training dataset against the current detector before anything is trained on them: the detector of [ModelPath](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ModelPath 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.ModelPath') is run over a seeded sample of \<b\>positive\</b\> Train and Validate images, and each label box is compared with the best\-overlapping detection by intersection over union\.

Reported as the mean, the median and the share at 0.5 or more, overall and per county part. An image the detector found nothing on counts as 0. A low mean means the new boxes are shifted or scaled against what the detector learned - stop and raise it on the tracking issue before training.

The first [LabelCheckOverlayCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.LabelCheckOverlayCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.LabelCheckOverlayCount') sampled images are also written, with the label box in green and the best detection in red, to a `label_check` folder under [ReportsDirectory](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ReportsDirectory 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.ReportsDirectory'). The sample is copied to a `label_check` folder beside the dataset, so the detector reads nothing but the sample; the dataset itself is not changed.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult? CheckYOLOTrainingDatasetLabels(this DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.CheckYOLOTrainingDatasetLabels(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken).yOLOTrainingDatasetOptions'></a>

`yOLOTrainingDatasetOptions` [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions')

The options naming the dataset, the weights, the interpreter and the sample\.

<a name='DiGi.GIS.YOLO.UI.Modify.CheckYOLOTrainingDatasetLabels(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the detector\.

#### Returns
[YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult')  
The check, or null when there are no options or no absolute dataset directory\.

<a name='DiGi.GIS.YOLO.UI.Modify.EvaluateYOLODetectors(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken)'></a>

## Modify\.EvaluateYOLODetectors\(this YOLOTrainingDatasetOptions, CancellationToken\) Method

Compares detector weights without the regressor: each weights file in [WeightsPaths](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.WeightsPaths 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.WeightsPaths') is run over the same Test images of a dataset, and its first detection years are scored against the labels by [YOLODetectorEvaluations\(IEnumerable&lt;DatasetReference&gt;, BoundingBoxResultFile, IEnumerable&lt;string&gt;, string, string, Range&lt;int&gt;\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_) 'DiGi\.GIS\.YOLO\.UI\.Query\.YOLODetectorEvaluations\(System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference\>, DiGi\.YOLO\.Classes\.BoundingBoxResultFile, System\.Collections\.Generic\.IEnumerable\<string\>, string, string, DiGi\.Core\.Classes\.Range\<int\>\)') \- one row per weights file for all Test buildings and one for the clean subset \(Test and not Legacy\)\.

Read-only: the detector runs over the dataset's images, its output goes to an `evaluation` folder beside them, and nothing is written to the Web API or to the dataset itself. The prediction threshold is [Confidence](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Confidence 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.Confidence'), the production one by default, so the rows measure the weights as the pipeline would run them.

A weights file that cannot be found or run is reported and stepped over, so the rows of the others still come back.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult? EvaluateYOLODetectors(this DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.EvaluateYOLODetectors(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken).yOLOTrainingDatasetOptions'></a>

`yOLOTrainingDatasetOptions` [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions')

The options naming the dataset, the weights files and the interpreter\.

<a name='DiGi.GIS.YOLO.UI.Modify.EvaluateYOLODetectors(thisDiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the detector\.

#### Returns
[YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult')  
The rows, or null when there are no options or no absolute dataset directory\.

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

Each step carries its own flag, so a run can be resumed without repeating the expensive ones, and the three write steps are off by default, so a first pass over a county reads and scores but stores nothing unless a write step is named on. Each step is idempotent: the scratch paths are derived from the county identifier, the detector overwrites its results file rather than appending to it, and a stored year built datum is read back and added to rather than replaced.

Only a building the detector fired on at least once is scored. A building it never fired on carries no per-year confidence series, which is the feature the regressor was built around, so scoring it would be scoring a row of absent features. The consequence is that the run predicts a year for fewer buildings than the file based workflow it replaces, which scored every row of its table - worth knowing before comparing the two reference by reference.

The scope is checked before any of it starts. A county identifier that is in no county row - most often a four character county code passed where an identifier was wanted - matches no stored building, so every step reports a legitimate zero and the run ends green having done nothing at all. That is a mis-scoped run rather than an empty county, so it fails here instead.

The options are checked against the model before any county is read. Narrowing them - asking for fewer years or radiuses than the model was trained on - drops features the model was fitted on, so every prediction silently degrades and it is refused. Widening them only adds features the model ignores, so it warns.

The scratch folder of a county that came through without a failed step is removed once the run has finished with it, unless [CleanScratchDirectory](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.CleanScratchDirectory 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions\.CleanScratchDirectory') says otherwise - so nothing downstream can depend on what a successful county left behind, which is the gap the two pass workflow used to carry. A county that failed keeps its folder, so re-running it costs seconds rather than repeating the export and the inference.

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

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken)'></a>

## Modify\.RunYOLOTrainingAsync\(this GISWebAPIManager, YOLOTrainingRunOptions, IProgress\<long\>, IProgress\<string\>, CancellationToken\) Method

Runs the detector retraining as one run: builds or appends to the training dataset, checks its labels, trains from the start weights, validates the result on the Test split and scores it against the other detectors\.

The steps run in the order of [YOLOTrainingStep](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLOTrainingStep') and the run stops at the first one that fails; [Steps](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions.Steps 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions\.Steps') narrows them. Every path is made absolute and every refusal that can be known up front - a missing start file, an unusable interpreter, a run name that is taken, a project folder inside a `YOLO\models` folder - is reported before the first step starts, with the option it concerns as the step name.

With [ResumeTraining](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions.ResumeTraining 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions\.ResumeTraining') the run continues the interrupted checkpoint in `<ProjectDirectory>\<RunName>\weights\last.pt` instead of starting a new one. The preflight then refuses a run without the [Train](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep.Train 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLOTrainingStep\.Train') step, a selected [Dataset](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep.Dataset 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLOTrainingStep\.Dataset') step, a completed run, a missing checkpoint, a finished checkpoint, a checkpoint whose dataset is gone and one whose recorded run folder was moved or renamed - each named by the option it concerns, before the training starts. The tail is identical to a fresh run, and the result reports the resume and the epoch it entered.

The trained weights are copied to `<ProjectDirectory>\<RunName>\<RunName>.pt`, a new file that is never overwritten and never named `model`; the validation and the evaluation measure that copy, and its SHA-256 is compared with the one the training reported. The identity of the start weights and of the copy is written to the log and to [information](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).information 'DiGi\.GIS\.YOLO\.UI\.Modify\.RunYOLOTrainingAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions, System\.IProgress\<long\>, System\.IProgress\<string\>, System\.Threading\.CancellationToken\)\.information') as soon as it is known. Without the training step the validation measures the start weights, which gives the baseline a candidate is compared with.

A cancellation is a result with [Cancelled](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult.Cancelled 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult\.Cancelled') set rather than an exception, and what earlier steps wrote is left as it is; the dataset manifest lets a re-run continue.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult?> RunYOLOTrainingAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions? yOLOTrainingRunOptions, System.IProgress<long>? progress=null, System.IProgress<string>? information=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The client of the Web API, needed only by the [Dataset](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep.Dataset 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLOTrainingStep\.Dataset') step\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).yOLOTrainingRunOptions'></a>

`yOLOTrainingRunOptions` [YOLOTrainingRunOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions')

The options naming the dataset, the start weights, the run and the steps\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).progress'></a>

`progress` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

Receives the number of buildings the dataset step has completed\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).information'></a>

`information` [System\.IProgress&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iprogress-1 'System\.IProgress\`1')

Receives one line for each thing worth reporting, without a prefix\.

<a name='DiGi.GIS.YOLO.UI.Modify.RunYOLOTrainingAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions,System.IProgress_long_,System.IProgress_string_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[YOLOTrainingRunResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The result, or null when there are no options\.

<a name='DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int)'></a>

## Modify\.SavePredictionImage\(this OrtoData, string, int, int\) Method

Decodes the orthophoto payload of an [DiGi\.GIS\.Classes\.OrtoData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata 'DiGi\.GIS\.Classes\.OrtoData') and saves it as a JPEG file, reporting the pixel size of the saved image\.

The one encoder both the inference export ([ExportPredictionImagesAsync\(this GISWebAPIManager, int, string, int, bool, CancellationToken\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.ExportPredictionImagesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,int,bool,System.Threading.CancellationToken) 'DiGi\.GIS\.YOLO\.UI\.Modify\.ExportPredictionImagesAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, string, int, bool, System\.Threading\.CancellationToken\)')) and the training dataset builder ([AppendYOLOTrainingDatasetAsync\(this GISWebAPIManager, YOLOTrainingDatasetOptions, IProgress&lt;long&gt;, CancellationToken\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Modify.AppendYOLOTrainingDatasetAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions,System.IProgress_long_,System.Threading.CancellationToken) 'DiGi\.GIS\.YOLO\.UI\.Modify\.AppendYOLOTrainingDatasetAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions, System\.IProgress\<long\>, System\.Threading\.CancellationToken\)')) save through, so the detector is trained on the same pixels it is later asked to score. The legacy builder encoded training images with WPF and normalised label boxes by the device-independent width of the image; this one reports [System\.Drawing\.Image\.Width](https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.width 'System\.Drawing\.Image\.Width') and [System\.Drawing\.Image\.Height](https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.height 'System\.Drawing\.Image\.Height'), which System.Drawing gives in pixels.

The file is overwritten when it exists. Deciding whether to skip it is the caller's business.

```csharp
public static bool SavePredictionImage(this DiGi.GIS.Classes.OrtoData? ortoData, string? path, out int width, out int height);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int).ortoData'></a>

`ortoData` [DiGi\.GIS\.Classes\.OrtoData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata 'DiGi\.GIS\.Classes\.OrtoData')

The orthophoto whose [DiGi\.GIS\.Classes\.OrtoData\.Bytes](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata.bytes 'DiGi\.GIS\.Classes\.OrtoData\.Bytes') are saved\.

<a name='DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path of the JPEG file to write\.

<a name='DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int).width'></a>

`width` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns true, the width of the saved image in pixels; otherwise 0\.

<a name='DiGi.GIS.YOLO.UI.Modify.SavePredictionImage(thisDiGi.GIS.Classes.OrtoData,string,int,int).height'></a>

`height` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

When this method returns true, the height of the saved image in pixels; otherwise 0\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True when the image was decoded and saved; false when there was no payload or no path\.

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

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.Building2DsAsync\(this GISWebAPIManager, int, IEnumerable\<string\>, int, PostOptions, CancellationToken\) Method

Reads the stored footprints of the named references, by reference\.

The read is bulk and paged at [referenceBatchSize](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).referenceBatchSize 'DiGi\.GIS\.YOLO\.UI\.Query\.Building2DsAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, System\.Collections\.Generic\.IEnumerable\<string\>, int, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)\.referenceBatchSize'). The endpoint has no `fallbackbyreference` parameter: it falls back to a lookup by reference alone only when no county is sent. So the county is sent first - it is the cheap, partition-pruned read - and the references it did not answer are asked for once more without it, which finds a footprint filed under a sibling polygon part of the county.

A page that cannot be read is logged and skipped; its references are simply absent from the result, and the caller counts them as buildings without a footprint.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string,DiGi.GIS.Classes.Building2D>?> Building2DsAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IEnumerable<string>? references, int referenceBatchSize=10000, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county part the references belong to\.

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The building references to read\.

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).referenceBatchSize'></a>

`referenceBatchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of references read in one request, at least 1\.

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the requests\.

<a name='DiGi.GIS.YOLO.UI.Query.Building2DsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the footprints by reference, or null when the client could not be built\.

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

<a name='DiGi.GIS.YOLO.UI.Query.DatasetReferences(string)'></a>

## Query\.DatasetReferences\(string\) Method

Reads the `dataset_references.tsv` manifest of a YOLO training dataset\.

A row that does not parse is skipped rather than failing the file: the manifest is appended to as the build goes, so a stopped run can leave a torn last line, and the building on it is then simply rebuilt. A reference named twice keeps its first row.

```csharp
public static System.Collections.Generic.List<DiGi.GIS.YOLO.UI.Classes.DatasetReference>? DatasetReferences(string? path);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.DatasetReferences(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path of the manifest\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
The dataset buildings in file order, or null when the file is missing, unreadable, or does not start with [DatasetReferences](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Header.DatasetReferences 'DiGi\.GIS\.YOLO\.UI\.Constants\.Header\.DatasetReferences')\.

<a name='DiGi.GIS.YOLO.UI.Query.IntersectionOverUnion(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.Geometry.Planar.Classes.BoundingBox2D)'></a>

## Query\.IntersectionOverUnion\(BoundingBox2D, BoundingBox2D\) Method

Computes the intersection over union of two axis\-aligned boxes: the area they share divided by the area they cover together\.

1 for identical boxes, 0 for boxes that do not overlap or only touch. Both boxes must be in the same coordinate space - the label check compares a label box and a detection, both in pixels of the same image.

```csharp
public static double IntersectionOverUnion(DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D_1, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D_2);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.IntersectionOverUnion(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.Geometry.Planar.Classes.BoundingBox2D).boundingBox2D_1'></a>

`boundingBox2D_1` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The first box\.

<a name='DiGi.GIS.YOLO.UI.Query.IntersectionOverUnion(DiGi.Geometry.Planar.Classes.BoundingBox2D,DiGi.Geometry.Planar.Classes.BoundingBox2D).boundingBox2D_2'></a>

`boundingBox2D_2` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The second box\.

#### Returns
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')  
The intersection over union in \[0, 1\], or [System\.Double\.NaN](https://learn.microsoft.com/en-us/dotnet/api/system.double.nan 'System\.Double\.NaN') when either box is missing or neither has any area\.

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

<a name='DiGi.GIS.YOLO.UI.Query.LegacyReferences(string)'></a>

## Query\.LegacyReferences\(string\) Method

Reads the building references named by a tab\-separated legacy reference list \- the regressor training table `Data_2025.05.27.tsv` from DiGi\.GIS\.ML\.

The column is found by its header, `Reference`, not by position, so a re-ordered table still reads. References are compared ordinally.

Null - never an empty set - when the file is missing, unreadable or has no `Reference` column: an empty set would report every held-out building as clean, which is the wrong direction to fail in.

```csharp
public static System.Collections.Generic.HashSet<string>? LegacyReferences(string? path);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.LegacyReferences(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path of the tab\-separated file\.

#### Returns
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')  
The references the file names, or null when it could not be read\.

<a name='DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset)'></a>

## Query\.LegacySource\(ISet\<string\>, string, IEnumerable\<YearBuiltData\>, DateTimeOffset\) Method

Decides whether a building may have been seen by the `train8` detector, and which record says so\.

The `train8` dataset is lost, so which buildings it learned from is reconstructed from two independent records, and the building is Legacy when <b>either</b> places it there:

- the legacy reference list (`Data_2025.05.27.tsv`, built from the same sources days after `train8`) names it;

- its stored history carries a user entry - of <b>any</b> relation, exact or bounded - that is undated or dated before [cutoff](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset).cutoff 'DiGi\.GIS\.YOLO\.UI\.Query\.LegacySource\(System\.Collections\.Generic\.ISet\<string\>, string, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Classes\.YearBuiltData\>, System\.DateTimeOffset\)\.cutoff'). Legacy entries were stored without a timestamp, and `train8` saw a building's images whatever year it was labelled with, so any user entry counts, not only the one that decides the label. Every row of every stored datum is looked at, because one reference can carry several.

A pure function, so the rule is testable without the Web API.

```csharp
public static DiGi.GIS.YOLO.UI.Enums.LegacySource LegacySource(System.Collections.Generic.ISet<string>? legacyReferences, string? reference, System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.YearBuiltData>? yearBuiltDatas, System.DateTimeOffset cutoff);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset).legacyReferences'></a>

`legacyReferences` [System\.Collections\.Generic\.ISet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1 'System\.Collections\.Generic\.ISet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1 'System\.Collections\.Generic\.ISet\`1')

The references the legacy reference list names, compared ordinally\. Null means no list, not an empty one \- the caller refuses to run without it\.

<a name='DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The building reference\.

<a name='DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset).yearBuiltDatas'></a>

`yearBuiltDatas` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.YearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.yearbuiltdata 'DiGi\.GIS\.Classes\.YearBuiltData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

Every stored year built datum of the building, or null when its history was not read\. Null answers only from the list\.

<a name='DiGi.GIS.YOLO.UI.Query.LegacySource(System.Collections.Generic.ISet_string_,string,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.YearBuiltData_,System.DateTimeOffset).cutoff'></a>

`cutoff` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The moment `train8` was saved\. An entry dated on or after it cannot have been seen\.

#### Returns
[LegacySource](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource')  
The record\(s\) placing the building in the `train8` dataset, or [None](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.None 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.None') when neither does\.

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

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.OrtoDatasAsync\(this GISWebAPIManager, int, string, PostOptions, CancellationToken\) Method

Reads the stored orthophotos of one building\.

The endpoint has no `fallbackbyreference` parameter and answers `204 No Content` for a building it does not hold under the county sent. So the county is sent first, and a building it does not answer is asked for once more without it, which finds imagery filed under a sibling polygon part of the county.

A failure is logged and answered with null, the same as a building with no imagery: the caller counts both as a building without imagery and steps over it.

```csharp
public static System.Threading.Tasks.Task<DiGi.GIS.Classes.OrtoDatas?> OrtoDatasAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, string? reference, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county part the building belongs to\.

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The building reference\.

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the requests\.

<a name='DiGi.GIS.YOLO.UI.Query.OrtoDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,string,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the building's orthophotos, or null when there are none or they could not be read\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool)'></a>

## Query\.PixelBoundingBox\(this OrtoData, BoundingBox2D, double, int, int, bool\) Method

Projects a building's world bounding box onto an orthophoto and returns it as a pixel rectangle clamped to the image\.

The box is cloned before it is grown by [offset](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).offset 'DiGi\.GIS\.YOLO\.UI\.Query\.PixelBoundingBox\(this DiGi\.GIS\.Classes\.OrtoData, DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D, double, int, int, bool\)\.offset'), so the same footprint box can be projected onto every year of a building without the offset accumulating. The top-left corner in world coordinates (minimum X, maximum Y) maps to the top-left corner of the image, because [DiGi\.GIS\.Classes\.OrtoData\.ToOrto\(DiGi\.Geometry\.Planar\.Classes\.Point2D\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata.toorto#digi-gis-classes-ortodata-toorto(digi-geometry-planar-classes-point2d) 'DiGi\.GIS\.Classes\.OrtoData\.ToOrto\(DiGi\.Geometry\.Planar\.Classes\.Point2D\)') flips the Y axis.

The rectangle is clamped to `[0, width] x [0, height]`. Ultralytics discards the whole image when one of its label boxes leaves the 0..1 range, so an unclamped box on a building at the edge of a crop would silently cost the image rather than just the part of the box outside it. A box that is wholly outside the image, or has no area left once clamped, is dropped.

```csharp
public static DiGi.Geometry.Planar.Classes.BoundingBox2D? PixelBoundingBox(this DiGi.GIS.Classes.OrtoData? ortoData, DiGi.Geometry.Planar.Classes.BoundingBox2D? boundingBox2D, double offset, int width, int height, out bool clamped);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).ortoData'></a>

`ortoData` [DiGi\.GIS\.Classes\.OrtoData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata 'DiGi\.GIS\.Classes\.OrtoData')

The orthophoto the box is projected onto\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).boundingBox2D'></a>

`boundingBox2D` [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')

The building's bounding box in world coordinates\. It is not modified\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance, in world units, the box is grown by on every side before it is projected\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).width'></a>

`width` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The width of the saved image in pixels\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).height'></a>

`height` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The height of the saved image in pixels\.

<a name='DiGi.GIS.YOLO.UI.Query.PixelBoundingBox(thisDiGi.GIS.Classes.OrtoData,DiGi.Geometry.Planar.Classes.BoundingBox2D,double,int,int,bool).clamped'></a>

`clamped` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

When this method returns, true if the projected rectangle crossed an image edge and was clamped\.

#### Returns
[DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D')  
The pixel rectangle, with [DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D\.Min](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.planar.classes.boundingbox2d.min 'DiGi\.Geometry\.Planar\.Classes\.BoundingBox2D\.Min') at its top\-left corner, or null when there is no image, no box, or no area left inside the image\.

<a name='DiGi.GIS.YOLO.UI.Query.ProgressCount(string)'></a>

## Query\.ProgressCount\(string\) Method

Reads the running total out of one line of the headless runner's standard output\.

The counterpart of [ProgressMessage\(long\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Create.ProgressMessage(long) 'DiGi\.GIS\.YOLO\.UI\.Create\.ProgressMessage\(long\)'). A caller pumping the runner's output passes every line through this and reports the ones that carry a count; a line that is not a progress line - a banner, a note, a tally - answers null rather than zero, so a caller cannot mistake other output for a run that has done nothing.

```csharp
public static System.Nullable<long> ProgressCount(string? line);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.ProgressCount(string).line'></a>

`line` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

One line of the runner's standard output\. Null, empty and unrecognised lines all answer null\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The running total the line reports, or null when the line is not a progress line\.

<a name='DiGi.GIS.YOLO.UI.Query.ReferenceDuplicatesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.ReferenceDuplicatesAsync\(this GISWebAPIManager, int, PostOptions, CancellationToken\) Method

Reads the building references filed under more than one county part, most collisions first\.

The endpoint is global - it takes no county - so the caller filters the rows by [DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferenceDuplicate\.CountyIds](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.building2dreferenceduplicate.countyids 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferenceDuplicate\.CountyIds'). A reference is unique only per county part and nothing enforces it, so this is the measurement of how many references a build must de-duplicate across parts.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.PostgreSQL.Classes.Building2DReferenceDuplicate>?> ReferenceDuplicatesAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int limit, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.ReferenceDuplicatesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.ReferenceDuplicatesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).limit'></a>

`limit` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The most rows to return, at least 1\.

<a name='DiGi.GIS.YOLO.UI.Query.ReferenceDuplicatesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the request\.

<a name='DiGi.GIS.YOLO.UI.Query.ReferenceDuplicatesAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferenceDuplicate](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.postgresql.classes.building2dreferenceduplicate 'DiGi\.GIS\.PostgreSQL\.Classes\.Building2DReferenceDuplicate')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the duplicated references, or null when they could not be read\.

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

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.StoredYearBuiltDatasAsync\(this GISWebAPIManager, int, IEnumerable\<string\>, int, ICollection\<string\>, PostOptions, CancellationToken\) Method

Reads every stored year built datum of the named references, grouped by reference\.

<b>All</b> rows of a reference are kept, in the order the read returned them. One reference can carry several stored rows - one per user among them - and a rule that looks at the history, such as the `train8` Legacy rule, has to see every one; a caller that wants a single datum takes the first, as [YearBuiltDatasAsync\(this GISWebAPIManager, int, IDictionary&lt;string,short&gt;, DateTimeOffset, bool, int, PostOptions, CancellationToken\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken) 'DiGi\.GIS\.YOLO\.UI\.Query\.YearBuiltDatasAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, System\.Collections\.Generic\.IDictionary\<string,short\>, System\.DateTimeOffset, bool, int, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)') does.

The read is bulk and paged at [referenceBatchSize](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).referenceBatchSize 'DiGi\.GIS\.YOLO\.UI\.Query\.StoredYearBuiltDatasAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, System\.Collections\.Generic\.IEnumerable\<string\>, int, System\.Collections\.Generic\.ICollection\<string\>, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)\.referenceBatchSize'), at most [YearBuiltDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.YearBuiltDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.YearBuiltDataReference\_Maximum') - the endpoint's cap. `fallbackbyreference=true` is sent explicitly, because the endpoint defaults it off and without it a row filed under a sibling polygon part of the county is not returned. The request body is passed as a factory, so a retry of a page rebuilds it rather than resending a drained stream.

A page is the unit that succeeds or fails. The references of a page that could not be read are added to [references\_Failed](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).references_Failed 'DiGi\.GIS\.YOLO\.UI\.Query\.StoredYearBuiltDatasAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, System\.Collections\.Generic\.IEnumerable\<string\>, int, System\.Collections\.Generic\.ICollection\<string\>, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)\.references\_Failed') and are absent from the result - a caller must not read their absence as "nothing stored".

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string,System.Collections.Generic.List<DiGi.GIS.Classes.YearBuiltData>>?> StoredYearBuiltDatasAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IEnumerable<string>? references, int referenceBatchSize=10000, System.Collections.Generic.ICollection<string>? references_Failed=null, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county row the references belong to\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The building references to read\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).referenceBatchSize'></a>

`referenceBatchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of references read in one request, clamped to \[1, [YearBuiltDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.YearBuiltDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.YearBuiltDataReference\_Maximum')\]\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).references_Failed'></a>

`references_Failed` [System\.Collections\.Generic\.ICollection&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1 'System\.Collections\.Generic\.ICollection\`1')

An optional collection the references of every page that could not be read are added to\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the requests\.

<a name='DiGi.GIS.YOLO.UI.Query.StoredYearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IEnumerable_string_,int,System.Collections.Generic.ICollection_string_,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.GIS\.Classes\.YearBuiltData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.yearbuiltdata 'DiGi\.GIS\.Classes\.YearBuiltData')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the stored rows by reference \- a reference with nothing stored is absent \- or null when the client could not be built\.

<a name='DiGi.GIS.YOLO.UI.Query.TryParseImageFileName(string,string,short)'></a>

## Query\.TryParseImageFileName\(string, string, short\) Method

Splits an orthophoto image file name of the form `{reference}_{year}.jpeg` into its building reference and year\.

The name is split at the <b>last</b> underscore, because a reference may itself contain underscores. The same rule `DiGi.GIS.YOLO.Create.Building2DYearBuiltPredictions` applies to the detector's output. A prefix test is not a substitute: `ABC_1_2015.jpeg` starts with `ABC_1` as well as with `ABC`, so matching a reference by `StartsWith` would claim another building's image.

The year is parsed with [System\.Globalization\.CultureInfo\.InvariantCulture](https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.invariantculture 'System\.Globalization\.CultureInfo\.InvariantCulture'), matching how the file name is written.

```csharp
public static bool TryParseImageFileName(string? fileName, out string? reference, out short year);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.TryParseImageFileName(string,string,short).fileName'></a>

`fileName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The file name or path\. The directory and extension are ignored\.

<a name='DiGi.GIS.YOLO.UI.Query.TryParseImageFileName(string,string,short).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

When this method returns true, the building reference; otherwise null\.

<a name='DiGi.GIS.YOLO.UI.Query.TryParseImageFileName(string,string,short).year'></a>

`year` [System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')

When this method returns true, the year of the orthophoto; otherwise 0\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True when the name carries a non\-empty reference and a year\.

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

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.UserYearBuiltsAsync\(this GISWebAPIManager, int, int, PostOptions, CancellationToken\) Method

Reads the training label of every labelled building of one county part: the stored `User year built` column of the building data, turned into one year per reference by [DiGi\.GIS\.IO\.Query\.YearBuiltLabels\(System\.Collections\.Generic\.IEnumerable\{DiGi\.Core\.IO\.Table\.Classes\.Table\}\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.query.yearbuiltlabels#digi-gis-io-query-yearbuiltlabels(system-collections-generic-ienumerable{digi-core-io-table-classes-table}) 'DiGi\.GIS\.IO\.Query\.YearBuiltLabels\(System\.Collections\.Generic\.IEnumerable\{DiGi\.Core\.IO\.Table\.Classes\.Table\}\)')\.

The same source and the same rule the Year Built regressor is trained on, so the detector and the regressor learn from identical labels: the most frequent <b>exact</b> user year, with bounded entries excluded. The column is written by the Year Built building data update, so that update has to have run since the last user edits for the labels to be current.

The county is paged by reference: each page is the next [pageSize](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).pageSize 'DiGi\.GIS\.YOLO\.UI\.Query\.UserYearBuiltsAsync\(this DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager, int, int, DiGi\.WebAPI\.Classes\.PostOptions, System\.Threading\.CancellationToken\)\.pageSize') rows after the last reference of the previous one, and a short page - or a blank last reference, which would otherwise restart the county - ends it. Only the label column is projected; the server adds the reference and county columns on top.

<b>Any page that cannot be read fails the whole county</b> - the result is null, never a partial label set, because a builder handed half a county's labels would build a dataset that silently misses the other half.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string,short>?> UserYearBuiltsAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, int pageSize=10000, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).gisWebAPIManager'></a>

`gisWebAPIManager` [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager')

The [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager') instance used to communicate with the WebAPI\.

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county part to read\.

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).pageSize'></a>

`pageSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of rows read in one request, clamped to \[1, [BuildingDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.BuildingDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.BuildingDataReference\_Maximum')\]\.

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).postOptions'></a>

`postOptions` [DiGi\.WebAPI\.Classes\.PostOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.postoptions 'DiGi\.WebAPI\.Classes\.PostOptions')

Optional configuration options for the requests\.

<a name='DiGi.GIS.YOLO.UI.Query.UserYearBuiltsAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken') to observe while waiting for the task to complete\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task returning the label year by reference \- an unlabelled building is absent \- or null when any page could not be read\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken)'></a>

## Query\.YearBuiltDatasAsync\(this GISWebAPIManager, int, IDictionary\<string,short\>, DateTimeOffset, bool, int, PostOptions, CancellationToken\) Method

Reads each building's stored year built data and adds the run's predicted construction year to it\.

The stored entry is read back rather than a fresh one built, because the year built table addresses a stored object by its own identifier: a datum built fresh carries a new one and is stored <i>alongside</i> whatever the building already holds instead of replacing it. Reading it back is also what preserves the history and any user-supplied year.

A building with nothing stored yet gets a new datum, which is the one case where a fresh identifier is correct.

Every prediction of one run carries the same stamp. The stored entries are keyed by it, so one stamp per run leaves one history entry per run, and re-running with the same stamp replaces that entry rather than adding to it.

The read is bulk: the endpoint answers up to [YearBuiltDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.YearBuiltDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.YearBuiltDataReference\_Maximum') references in one request, so the references are paged at that size and a page is the unit that succeeds or fails. A page that cannot be read is skipped rather than answered with a fresh datum for every building of it, because that would store a second row alongside the one that could not be read.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.List<DiGi.GIS.Classes.YearBuiltData>> YearBuiltDatasAsync(this DiGi.GIS.WebAPI.Classes.GISWebAPIManager? gisWebAPIManager, int countyId, System.Collections.Generic.IDictionary<string,short>? years, System.DateTimeOffset runTimestamp, bool readStored=true, int referenceBatchSize=10000, DiGi.WebAPI.Classes.PostOptions? postOptions=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
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

When true, each building's stored entry is read back first so the prediction is added to its history\. Set it false only when the caller is not storing the year built data at all \- a county is tens of thousands of buildings, and the building data column is derived from the latest prediction, which a fresh entry already carries\.

<a name='DiGi.GIS.YOLO.UI.Query.YearBuiltDatasAsync(thisDiGi.GIS.WebAPI.Classes.GISWebAPIManager,int,System.Collections.Generic.IDictionary_string,short_,System.DateTimeOffset,bool,int,DiGi.WebAPI.Classes.PostOptions,System.Threading.CancellationToken).referenceBatchSize'></a>

`referenceBatchSize` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of references read in one request, at most [YearBuiltDataReference\_Maximum](DiGi.GIS.YOLO.UI.Constants.md#DiGi.GIS.YOLO.UI.Constants.Count.YearBuiltDataReference_Maximum 'DiGi\.GIS\.YOLO\.UI\.Constants\.Count\.YearBuiltDataReference\_Maximum') \- the endpoint's cap\.

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

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_)'></a>

## Query\.YOLODetectorEvaluations\(IEnumerable\<DatasetReference\>, BoundingBoxResultFile, IEnumerable\<string\>, string, string, Range\<int\>\) Method

Scores one detector's detections over the Test buildings of a dataset: the first detection year of each building against its label, as mean absolute error, root mean square error and exact\-match share, for all Test buildings and for the clean subset\.

The detections go through the <b>same in-memory columns</b> the pipeline writes into the building data - [DiGi\.GIS\.YOLO\.Create\.Building2DYearBuiltPredictions\(DiGi\.YOLO\.Classes\.BoundingBoxResultFile\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.create.building2dyearbuiltpredictions#digi-gis-yolo-create-building2dyearbuiltpredictions(digi-yolo-classes-boundingboxresultfile) 'DiGi\.GIS\.YOLO\.Create\.Building2DYearBuiltPredictions\(DiGi\.YOLO\.Classes\.BoundingBoxResultFile\)') then [DiGi\.GIS\.IO\.Modify\.Update\_Building2D\_YearBuiltPredictions\(DiGi\.Core\.IO\.Table\.Classes\.Table,System\.Int32,System\.Collections\.Generic\.IEnumerable\{DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\}\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.modify.update_building2d_yearbuiltpredictions#digi-gis-io-modify-update_building2d_yearbuiltpredictions(digi-core-io-table-classes-table-system-int32-system-collections-generic-ienumerable{digi-gis-classes-building2dyearbuiltpredictions}) 'DiGi\.GIS\.IO\.Modify\.Update\_Building2D\_YearBuiltPredictions\(DiGi\.Core\.IO\.Table\.Classes\.Table,System\.Int32,System\.Collections\.Generic\.IEnumerable\{DiGi\.GIS\.Classes\.Building2DYearBuiltPredictions\}\)') on an empty table - and the first detection year is read back by [DiGi\.GIS\.IO\.Query\.FirstDetectionYears\(DiGi\.Core\.IO\.Table\.Classes\.Table,DiGi\.Core\.Classes\.Range\{System\.Int32\},System\.Int16\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.query.firstdetectionyears#digi-gis-io-query-firstdetectionyears(digi-core-io-table-classes-table-digi-core-classes-range{system-int32}-system-int16) 'DiGi\.GIS\.IO\.Query\.FirstDetectionYears\(DiGi\.Core\.IO\.Table\.Classes\.Table,DiGi\.Core\.Classes\.Range\{System\.Int32\},System\.Int16\)'), so the number measured here is the feature the regressor is given, not a re-implementation of it. Nothing is written anywhere.

A Test building with images the detector fired on in no year is still scored - its first detection year is the start of the range, as the regressor sees it. A building with no Test image is not in either subset. The clean subset is Test and not Legacy; a building whose Legacy decision could not be taken is Legacy.

```csharp
public static System.Collections.Generic.List<DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation> YOLODetectorEvaluations(System.Collections.Generic.IEnumerable<DiGi.GIS.YOLO.UI.Classes.DatasetReference>? datasetReferences, DiGi.YOLO.Classes.BoundingBoxResultFile? boundingBoxResultFile, System.Collections.Generic.IEnumerable<string>? references_Imaged, string? weightsPath, string? sHA256, DiGi.Core.Classes.Range<int>? years=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).datasetReferences'></a>

`datasetReferences` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The dataset manifest\. Only its Test buildings are scored\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).boundingBoxResultFile'></a>

`boundingBoxResultFile` [DiGi\.YOLO\.Classes\.BoundingBoxResultFile](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.classes.boundingboxresultfile 'DiGi\.YOLO\.Classes\.BoundingBoxResultFile')

The detector's output over the Test images\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).references_Imaged'></a>

`references_Imaged` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The references with at least one Test image \- the population scored\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).weightsPath'></a>

`weightsPath` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The weights file the detections came from, carried into the rows\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).sHA256'></a>

`sHA256` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The SHA\-256 of the weights file, carried into the rows\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLODetectorEvaluations(System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.DatasetReference_,DiGi.YOLO.Classes.BoundingBoxResultFile,System.Collections.Generic.IEnumerable_string_,string,string,DiGi.Core.Classes.Range_int_).years'></a>

`years` [DiGi\.Core\.Classes\.Range&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')

The range of years first detections are read over\. Null uses 2008 to 2025\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
Two rows \- [All](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset.All 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLODetectorEvaluationSubset\.All') then [Clean](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset.Clean 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLODetectorEvaluationSubset\.Clean') \- or an empty list when there is no manifest\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingDatasetOptions(string)'></a>

## Query\.YOLOTrainingDatasetOptions\(string\) Method

Reads and deserializes the [YOLOTrainingDatasetOptions\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YOLOTrainingDatasetOptions(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.YOLOTrainingDatasetOptions\(string\)') from the specified path or default locations\.

A member the file does not name keeps the class default, and a key the class does not declare is dropped in silence - so a misspelt flag reads as an unchanged one. The committed template beside the deployed application is the authority on the spelling.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions? YOLOTrainingDatasetOptions(string? path=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingDatasetOptions(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional file path to YOLOTrainingDatasetOptions\.json\. If omitted, [ConfigurationFilePath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ConfigurationFilePath\(string\)') resolves it against the deployed output\.

#### Returns
[YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions')  
The deserialized options instance, or null if not found or invalid\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingRunExitCode(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult)'></a>

## Query\.YOLOTrainingRunExitCode\(YOLOTrainingRunResult\) Method

Maps the outcome of a `--train` run to the exit code of the console application\.

A cancellation is [Cancelled](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Cancelled 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Cancelled') whatever else was listed. Otherwise an option that cannot be used is a [Configuration](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Configuration 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Configuration'), a machine that cannot run the detector an [Environment](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Environment 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Environment'), the training and the validation have codes of their own, and any other failed step is [Failed](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Failed 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Failed'). The earlier reading of the same step wins, so a run refused before its first step keeps the code of what was wrong with the options.

```csharp
public static DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode YOLOTrainingRunExitCode(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult? yOLOTrainingRunResult);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingRunExitCode(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult).yOLOTrainingRunResult'></a>

`yOLOTrainingRunResult` [YOLOTrainingRunResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult')

The result of the run\.

#### Returns
[YearBuiltPredictionExitCode](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode')  
The exit code\. [Failed](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Failed 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Failed') when there is no result\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingRunOptions(string)'></a>

## Query\.YOLOTrainingRunOptions\(string\) Method

Reads and deserializes the [YOLOTrainingRunOptions\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YOLOTrainingRunOptions(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.YOLOTrainingRunOptions\(string\)') from the specified path or default locations\.

The nested [DatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions.DatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions\.DatasetOptions') is written as a plain object, exactly as in a [YOLOTrainingDatasetOptions\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.YOLOTrainingDatasetOptions(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.YOLOTrainingDatasetOptions\(string\)') file. A member the file does not name keeps the class default, and a key the class does not declare is dropped in silence - so a misspelt flag reads as an unchanged one. The committed template beside the deployed application is the authority on the spelling.

```csharp
public static DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions? YOLOTrainingRunOptions(string? path=null);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingRunOptions(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The optional file path to YOLOTrainingRunOptions\.json\. If omitted, [ConfigurationFilePath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ConfigurationFilePath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ConfigurationFilePath\(string\)') resolves it against the deployed output\.

#### Returns
[YOLOTrainingRunOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions')  
The deserialized options instance, or null if not found or invalid\.

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingStepName(DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep)'></a>

## Query\.YOLOTrainingStepName\(YOLOTrainingStep\) Method

Gives the name under which a failed step of the `--train` mode is listed in [FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingRunResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult\.FailedStepNames')\.

The name is prefixed with the enumeration, because the dataset and evaluation steps list their own failures by bare names such as `Train` and `Test` (the dataset splits), and a step called `Train` would be read as one of those.

```csharp
public static string YOLOTrainingStepName(DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep yOLOTrainingStep);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Query.YOLOTrainingStepName(DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep).yOLOTrainingStep'></a>

`yOLOTrainingStep` [YOLOTrainingStep](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLOTrainingStep 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLOTrainingStep')

The step\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The name, such as `YOLOTrainingStep.Train`\.