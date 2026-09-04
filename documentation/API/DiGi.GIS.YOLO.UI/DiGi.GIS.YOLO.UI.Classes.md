#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Classes Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions'></a>

## YearBuiltPredictionPipelineOptions Class

Provides the settings one unattended run of the Year Built prediction pipeline needs: which counties it covers, where it keeps its scratch files, which weights and interpreter score the imagery, and which of its steps actually run\.

Every step carries its own flag so a run can be resumed without repeating the expensive ones. The three write steps are off by default, so a first pass over a county is harmless - the run reads everything, scores everything and stores nothing.

There is deliberately no member for the Web API key. These options are written to disk as JSON and the key is a secret, so it travels on [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager\.Key](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager.key 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager\.Key'), which the host reads from a git-ignored configuration file.

```csharp
public class YearBuiltPredictionPipelineOptions : DiGi.Core.Classes.SerializableOptions, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → YearBuiltPredictionPipelineOptions

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.YearBuiltPredictionPipelineOptions()'></a>

## YearBuiltPredictionPipelineOptions\(\) Constructor

Initializes a new instance of the [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions') class with default values\.

```csharp
public YearBuiltPredictionPipelineOptions();
```

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.YearBuiltPredictionPipelineOptions(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions)'></a>

## YearBuiltPredictionPipelineOptions\(YearBuiltPredictionPipelineOptions\) Constructor

Initializes a new instance of the [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions') class by copying an existing options instance\.

```csharp
public YearBuiltPredictionPipelineOptions(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions? yearBuiltPredictionPipelineOptions);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.YearBuiltPredictionPipelineOptions(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions).yearBuiltPredictionPipelineOptions'></a>

`yearBuiltPredictionPipelineOptions` [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions')

The source options instance to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.YearBuiltPredictionPipelineOptions(System.Text.Json.Nodes.JsonObject)'></a>

## YearBuiltPredictionPipelineOptions\(JsonObject\) Constructor

Initializes a new instance of the [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions') class using a JSON object\.

```csharp
public YearBuiltPredictionPipelineOptions(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.YearBuiltPredictionPipelineOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the configuration settings\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.BatchSize'></a>

## YearBuiltPredictionPipelineOptions\.BatchSize Property

Gets or sets the number of buildings whose detections or predictions are sent in one request\.

A county carries ninety-odd detection columns over tens of thousands of buildings, so the writes are batched rather than sent as one body.

```csharp
public int BatchSize { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.CleanScratchDirectory'></a>

## YearBuiltPredictionPipelineOptions\.CleanScratchDirectory Property

Gets or sets whether each county's scratch folder \- the imagery exported for it and the detection results written from it \- is deleted once the run has finished with that county\.

On by default, so a run leaves nothing behind. The alternative is what this replaced: the scoring step rebuilds its list of buildings from the results file on disk rather than from the stored detections, so a county whose scratch folder went missing between two separate runs was skipped in silence even though its detection columns were already stored. A run that always cleans up has no between.

Only a county that came through without a failed step is cleaned. One that failed keeps its imagery and its detections, so re-running it costs seconds rather than the half hour of export and hour and a half of inference that produced them. The feature coverage refusal is the case that makes this worth the extra condition: it is a configuration error, it is reproducible, and it fires only after both of those steps have already been paid for. A cancelled county is cleaned - stopping a run is a deliberate act, and what it leaves behind is not a partial success.

Turn it off for the split detections-then-score workflow, whose second run reads the first run's results file, and for a run that is meant to be resumed. The committed split templates set it to false for exactly that reason.

```csharp
public bool CleanScratchDirectory { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Confidence'></a>

## YearBuiltPredictionPipelineOptions\.Confidence Property

Gets or sets the confidence threshold a detection has to reach to be reported, passed to the prediction script as \-\-conf\.

The default matches the script's own default. The weights are frozen, so this is the only knob over how much the detector reports.

```csharp
public double Confidence { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.CountyIds'></a>

## YearBuiltPredictionPipelineOptions\.CountyIds Property

Gets or sets the county rows the run covers, by identifier\.

Identifiers rather than codes, and each identifier is a polygon part: a county whose territory is in several pieces is held as one row per piece. Name every part of a county, so the parts are recognised as siblings and each written row is filed under the part its reference belongs to.

There is no run-everything default. The pipeline writes deployed data, so the scope is always stated.

```csharp
public System.Collections.Generic.HashSet<int>? CountyIds { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.ExportImages'></a>

## YearBuiltPredictionPipelineOptions\.ExportImages Property

Gets or sets whether the orthophoto imagery is exported to the scratch directory before the detector runs\.

Turn it off to score imagery a previous run already wrote. With [Resume](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Resume 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions\.Resume') set the export skips what is on disk anyway, so leaving it on costs one listing request per county.

```csharp
public bool ExportImages { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.MaxConcurrentRequests'></a>

## YearBuiltPredictionPipelineOptions\.MaxConcurrentRequests Property

Gets or sets how many Web API requests may be in flight at once\.

```csharp
public int MaxConcurrentRequests { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.ModelPath'></a>

## YearBuiltPredictionPipelineOptions\.ModelPath Property

Gets or sets the path of the trained weights the detector scores with\.

Left null the script falls back to its own search, which picks whichever training run is newest on disk. Name the file, so a run is reproducible.

```csharp
public string? ModelPath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.PythonPath'></a>

## YearBuiltPredictionPipelineOptions\.PythonPath Property

Gets or sets the path of the CPython interpreter that runs the prediction script, or the name of one on PATH\.

This has to be CPython with ultralytics and torch installed. The IronPython engine in DiGi.Scripting.Python can host neither.

```csharp
public string? PythonPath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Radiuses'></a>

## YearBuiltPredictionPipelineOptions\.Radiuses Property

Gets or sets the radiuses the radial ratio features cover, in metres\.

Carried for the same reason as [Years](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Years 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions\.Years'): it decides which columns the feature projection asks for, and a projection that disagrees with the range the regressor was trained on hands the model defaults rather than features - which scores without failing. Null means the same default the column list itself applies.

```csharp
public System.Collections.Generic.List<double>? Radiuses { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.ReferenceBatchSize'></a>

## YearBuiltPredictionPipelineOptions\.ReferenceBatchSize Property

Gets or sets how many references a bulk read is asked for in one request\.

The feature table and the year built data share the cap - each endpoint refuses more than ten thousand references at a time - and a county is thirty to a hundred and fifty thousand buildings, so both reads are paged. A larger value is clamped down to the cap while the run works.

```csharp
public int ReferenceBatchSize { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Resume'></a>

## YearBuiltPredictionPipelineOptions\.Resume Property

Gets or sets whether work a previous run already did is skipped rather than repeated\.

Governs the image export, which is the expensive step: an image already on disk is neither fetched nor re-encoded.

```csharp
public bool Resume { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.RunPrediction'></a>

## YearBuiltPredictionPipelineOptions\.RunPrediction Property

Gets or sets whether the detector is run over the exported imagery\.

Turn it off to re-use the detections a previous run wrote to the scratch directory. The results file is opened for writing rather than appending, so a repeated run replaces the previous answer instead of doubling it.

```csharp
public bool RunPrediction { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Score'></a>

## YearBuiltPredictionPipelineOptions\.Score Property

Gets or sets whether the building features are read and scored into predicted construction years\.

Requires an implementation of [DiGi\.GIS\.IO\.Interfaces\.IYearBuiltPredictor](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.interfaces.iyearbuiltpredictor 'DiGi\.GIS\.IO\.Interfaces\.IYearBuiltPredictor'). With it off the run stops after the detections, which is the shape of a detection-only pass.

```csharp
public bool Score { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.ScratchDirectory'></a>

## YearBuiltPredictionPipelineOptions\.ScratchDirectory Property

Gets or sets the directory the run keeps its imagery and its detection results in\.

Each county gets its own folder underneath, named after the county identifier, so two counties cannot score each other's imagery and a resumed run finds what it left behind.

```csharp
public string? ScratchDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.UpdateDetections'></a>

## YearBuiltPredictionPipelineOptions\.UpdateDetections Property

Gets or sets whether the detection features are written into the stored building data\.

```csharp
public bool UpdateDetections { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.UpdatePredictedYearBuilt'></a>

## YearBuiltPredictionPipelineOptions\.UpdatePredictedYearBuilt Property

Gets or sets whether the latest predicted construction year is written into the building data column\.

Written from the same merged year built data the history step builds, so the column and the history cannot disagree.

```csharp
public bool UpdatePredictedYearBuilt { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.UpdateYearBuiltData'></a>

## YearBuiltPredictionPipelineOptions\.UpdateYearBuiltData Property

Gets or sets whether the dated prediction is written into the year built data, preserving the history\.

The stored entry is read back and added to rather than replaced, because a year built datum built fresh carries a new identifier and would be stored alongside the building's existing one rather than in place of it.

```csharp
public bool UpdateYearBuiltData { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.WorkingDirectory'></a>

## YearBuiltPredictionPipelineOptions\.WorkingDirectory Property

Gets or sets the directory the prediction process runs in, which is also where the runner keeps the Python scripts\.

The prediction script imports its helper module from the directory it sits in, so the two files have to stay together. Ultralytics also writes its own caches here.

```csharp
public string? WorkingDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Years'></a>

## YearBuiltPredictionPipelineOptions\.Years Property

Gets or sets the range of years the detection and temporal features cover\.

Has to match the range the regressor was trained on, because it decides which columns the feature projection asks for. Null means the same default the column list itself applies.

```csharp
public DiGi.Core.Classes.Range<int>? Years { get; set; }
```

#### Property Value
[DiGi\.Core\.Classes\.Range&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult'></a>

## YearBuiltPredictionResult Class

What one run of the Year Built prediction pipeline did: how much it read, how much it scored, how much it stored, and what it could not finish\.

[FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult\.FailedStepNames') is what says whether a run did everything it set out to do. A step that fails is logged and stepped over so the steps behind it still run, so a result that came back at all is not by itself evidence of a complete run.

[RunTimestamp](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.RunTimestamp 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult\.RunTimestamp') is the stamp every prediction of the run carries into the year built data. One stamp for the whole run is deliberate: the stored entries are keyed by it, so a stamp taken per building would write one history entry per building instead of one per run.

```csharp
public class YearBuiltPredictionResult : DiGi.Core.Classes.SerializableResult, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableresult 'DiGi\.Core\.Classes\.SerializableResult') → YearBuiltPredictionResult

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult)'></a>

## YearBuiltPredictionResult\(YearBuiltPredictionResult\) Constructor

Initializes a new instance of the [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult') class by copying an existing one\.

```csharp
public YearBuiltPredictionResult(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult? yearBuiltPredictionResult);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult).yearBuiltPredictionResult'></a>

`yearBuiltPredictionResult` [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult')

The [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool)'></a>

## YearBuiltPredictionResult\(IEnumerable\<int\>, Nullable\<DateTimeOffset\>, Nullable\<DateTimeOffset\>, Nullable\<DateTimeOffset\>, long, long, long, long, long, long, long, IEnumerable\<string\>, IEnumerable\<string\>, bool\) Constructor

Initializes a new instance of the [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult') class\.

```csharp
public YearBuiltPredictionResult(System.Collections.Generic.IEnumerable<int>? countyIds, System.Nullable<System.DateTimeOffset> runTimestamp, System.Nullable<System.DateTimeOffset> start, System.Nullable<System.DateTimeOffset> end, long imageCount, long detectionCount, long buildingCount, long featureRowCount, long predictionCount, long yearBuiltDataUpdatedCount, long buildingDataUpdatedCount, System.Collections.Generic.IEnumerable<string>? failedStepNames, System.Collections.Generic.IEnumerable<string>? messages, bool cancelled);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The county rows the run covered, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).runTimestamp'></a>

`runTimestamp` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The stamp every prediction of the run carries, or null when nothing was scored\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).start'></a>

`start` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run started\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).end'></a>

`end` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run ended\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).imageCount'></a>

`imageCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of orthophoto images the detector was given\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).detectionCount'></a>

`detectionCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of detections the detector reported\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).buildingCount'></a>

`buildingCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of buildings carrying at least one detection\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).featureRowCount'></a>

`featureRowCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of building data rows read for scoring\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).predictionCount'></a>

`predictionCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of construction years the regressor returned\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).yearBuiltDataUpdatedCount'></a>

`yearBuiltDataUpdatedCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of year built data entries written\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).buildingDataUpdatedCount'></a>

`buildingDataUpdatedCount` [System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

The number of building data rows written\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).failedStepNames'></a>

`failedStepNames` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The steps that reported a failure, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).messages'></a>

`messages` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

What the run has to say beyond its tallies, or null for nothing\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Collections.Generic.IEnumerable_int_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,long,long,long,long,long,long,long,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,bool).cancelled'></a>

`cancelled` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether the run was stopped before it covered everything it was given\.

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Text.Json.Nodes.JsonObject)'></a>

## YearBuiltPredictionResult\(JsonObject\) Constructor

Initializes a new instance of the [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YearBuiltPredictionResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltPredictionResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.BuildingCount'></a>

## YearBuiltPredictionResult\.BuildingCount Property

Gets the number of buildings carrying at least one detection\.

Lower than the number of images, because one building is imaged once per year of orthophoto coverage, and lower than the number of buildings in the county, because a building the detector found nothing on in any year is not counted.

```csharp
public long BuildingCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.BuildingDataUpdatedCount'></a>

## YearBuiltPredictionResult\.BuildingDataUpdatedCount Property

Gets the number of building data rows written, counting the detection write and the predicted year column separately\.

```csharp
public long BuildingDataUpdatedCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.Cancelled'></a>

## YearBuiltPredictionResult\.Cancelled Property

Gets whether the run was stopped before it covered everything it was given\.

```csharp
public bool Cancelled { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.CountyIds'></a>

## YearBuiltPredictionResult\.CountyIds Property

Gets the county rows the run covered\.

Each identifier is a polygon part rather than a county, so a multi-part county appears here once per part.

```csharp
public System.Collections.Generic.List<int> CountyIds { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.DetectionCount'></a>

## YearBuiltPredictionResult\.DetectionCount Property

Gets the number of detections the detector reported, across every building and every year\.

```csharp
public long DetectionCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.Duration'></a>

## YearBuiltPredictionResult\.Duration Property

Gets the duration of the run, or null when it did not record both ends\.

```csharp
public System.Nullable<System.TimeSpan> Duration { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.End'></a>

## YearBuiltPredictionResult\.End Property

Gets when the run ended\.

```csharp
public System.Nullable<System.DateTimeOffset> End { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.FailedStepNames'></a>

## YearBuiltPredictionResult\.FailedStepNames Property

Gets the steps that reported a failure and were stepped over\.

Empty is the only evidence that a run did everything it set out to do - the result comes back either way.

```csharp
public System.Collections.Generic.List<string> FailedStepNames { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.FeatureRowCount'></a>

## YearBuiltPredictionResult\.FeatureRowCount Property

Gets the number of building data rows read for scoring\.

```csharp
public long FeatureRowCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.ImageCount'></a>

## YearBuiltPredictionResult\.ImageCount Property

Gets the number of orthophoto images the detector was given\.

```csharp
public long ImageCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.Messages'></a>

## YearBuiltPredictionResult\.Messages Property

Gets what the run has to say beyond its tallies, such as why the machine could not run the detector at all\.

```csharp
public System.Collections.Generic.List<string> Messages { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.PredictionCount'></a>

## YearBuiltPredictionResult\.PredictionCount Property

Gets the number of construction years the regressor returned\.

```csharp
public long PredictionCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.RunTimestamp'></a>

## YearBuiltPredictionResult\.RunTimestamp Property

Gets the stamp every prediction of the run carries into the year built data, or null when nothing was scored\.

One stamp for the whole run. The stored entries are keyed by it, so re-running with the same stamp replaces the run rather than adding to the history, and a stamp taken per building would write one entry per building.

```csharp
public System.Nullable<System.DateTimeOffset> RunTimestamp { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.Start'></a>

## YearBuiltPredictionResult\.Start Property

Gets when the run started\.

```csharp
public System.Nullable<System.DateTimeOffset> Start { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult.YearBuiltDataUpdatedCount'></a>

## YearBuiltPredictionResult\.YearBuiltDataUpdatedCount Property

Gets the number of year built data entries written, preserving each building's history\.

```csharp
public long YearBuiltDataUpdatedCount { get; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')