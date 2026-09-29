#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Classes Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference'></a>

## DatasetReference Class

One building of a YOLO training dataset: the county part it was read from, the split it went to, its label year, and whether the `train8` detector may have seen it\.

One row of the `dataset_references.tsv` manifest. The label check and the detector evaluation read the dataset through it, so the split and the Legacy decision are taken once, when the dataset is built, and never re-derived.

```csharp
public class DatasetReference : DiGi.Core.Classes.SerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → DatasetReference

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(DiGi.GIS.YOLO.UI.Classes.DatasetReference)'></a>

## DatasetReference\(DatasetReference\) Constructor

Initializes a new instance of the [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference') class by copying an existing one\.

```csharp
public DatasetReference(DiGi.GIS.YOLO.UI.Classes.DatasetReference? datasetReference);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(DiGi.GIS.YOLO.UI.Classes.DatasetReference).datasetReference'></a>

`datasetReference` [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference')

The [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource)'></a>

## DatasetReference\(string, int, Category, short, LegacySource\) Constructor

Initializes a new instance of the [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference') class\.

```csharp
public DatasetReference(string? reference, int countyId, DiGi.YOLO.Enums.Category category, short label, DiGi.GIS.YOLO.UI.Enums.LegacySource legacySource);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The building reference\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource).countyId'></a>

`countyId` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The identifier of the county part the building was read from\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource).category'></a>

`category` [DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')

The split the building went to\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource).label'></a>

`label` [System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')

The label year \- the building's exact user year built\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(string,int,DiGi.YOLO.Enums.Category,short,DiGi.GIS.YOLO.UI.Enums.LegacySource).legacySource'></a>

`legacySource` [LegacySource](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource')

Which record says the `train8` detector may have seen the building\.

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(System.Text.Json.Nodes.JsonObject)'></a>

## DatasetReference\(JsonObject\) Constructor

Initializes a new instance of the [DatasetReference](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.DatasetReference 'DiGi\.GIS\.YOLO\.UI\.Classes\.DatasetReference') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public DatasetReference(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.DatasetReference(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.Category'></a>

## DatasetReference\.Category Property

Gets the split the building went to\. A held\-out building is always [DiGi\.YOLO\.Enums\.Category\.Test](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category.test 'DiGi\.YOLO\.Enums\.Category\.Test')\.

```csharp
public DiGi.YOLO.Enums.Category Category { get; }
```

#### Property Value
[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.CountyId'></a>

## DatasetReference\.CountyId Property

Gets the identifier of the county part the building was read from\.

```csharp
public int CountyId { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.Label'></a>

## DatasetReference\.Label Property

Gets the label year \- the building's exact user year built\. An image of a year before it is a negative, one of this year or later carries the building's box\.

```csharp
public short Label { get; }
```

#### Property Value
[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.Legacy'></a>

## DatasetReference\.Legacy Property

Gets whether the `train8` detector may have seen the building \- true for anything but [None](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.None 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.None'), including [Unknown](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.Unknown 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.Unknown')\.

```csharp
public bool Legacy { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.LegacySource'></a>

## DatasetReference\.LegacySource Property

Gets which record says the `train8` detector may have seen the building\.

For a Train or Validate building only the legacy reference list is consulted, because their Legacy decision feeds no metric.

```csharp
public DiGi.GIS.YOLO.UI.Enums.LegacySource LegacySource { get; }
```

#### Property Value
[LegacySource](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource')

<a name='DiGi.GIS.YOLO.UI.Classes.DatasetReference.Reference'></a>

## DatasetReference\.Reference Property

Gets the building reference\.

```csharp
public string? Reference { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

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

Gets or sets the path of the trained weights the detector scores with, resolved against the runner by [ModelPath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ModelPath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ModelPath\(string\)')\.

The default is where the deployment puts them: `CopyUserFiles` flattens the git-ignored `user files` folder into the runner's output, and the resolver strips that segment, so the same value names the weights in a workspace checkout and beside a deployed executable. Every committed options template carries it too.

<b>Null is not a fallback search.</b> A run whose weights are not a file that exists is refused outright, so a null path is a county that exports its imagery and then fails - which is what a defaulted path is here to prevent. A caller that means to score with different weights names them.

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

Carried for the same reason as [Years](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions.Years 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions\.Years'): it decides which columns the feature projection asks for, and is checked against the predictor's stated contract before a run - narrower is refused, wider warns. Null means the same default the column list itself applies.

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

Gets or sets whether the three year built columns \- predicted, user and calculated \- are written into the building data\.

Written from the same merged year built data the history step builds, so the columns and the history cannot disagree.

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

Decides which columns the feature projection asks for, and is checked against the predictor's stated contract before a run: narrower than the model is refused, wider only adds features the model ignores. Null means the same default the column list itself applies.

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

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation'></a>

## YOLODetectorEvaluation Class

How well one detector's first detection year matches the label year over one subset of the Test buildings\.

The first detection year is the first orthophoto year the detector finds the building in - the feature the Year Built regressor leans on most - so comparing it with the label measures the detector without the regressor, which was fitted to the old detector and would penalise any change.

```csharp
public class YOLODetectorEvaluation : DiGi.Core.Classes.SerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → YOLODetectorEvaluation

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation)'></a>

## YOLODetectorEvaluation\(YOLODetectorEvaluation\) Constructor

Initializes a new instance of the [YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation') class by copying an existing one\.

```csharp
public YOLODetectorEvaluation(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation? yOLODetectorEvaluation);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation).yOLODetectorEvaluation'></a>

`yOLODetectorEvaluation` [YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation')

The [YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double)'></a>

## YOLODetectorEvaluation\(string, string, YOLODetectorEvaluationSubset, int, double, double, double\) Constructor

Initializes a new instance of the [YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation') class\.

```csharp
public YOLODetectorEvaluation(string? weightsPath, string? sHA256, DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset subset, int count, double meanAbsoluteError, double rootMeanSquareError, double exactShare);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).weightsPath'></a>

`weightsPath` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The weights file evaluated\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).sHA256'></a>

`sHA256` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The SHA\-256 of the weights file, in lower\-case hexadecimal\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).subset'></a>

`subset` [YOLODetectorEvaluationSubset](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLODetectorEvaluationSubset')

The subset of the Test buildings the row covers\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of buildings in the subset\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).meanAbsoluteError'></a>

`meanAbsoluteError` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The mean absolute difference between the first detection year and the label, in years\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).rootMeanSquareError'></a>

`rootMeanSquareError` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The root mean square difference between the first detection year and the label, in years\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(string,string,DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset,int,double,double,double).exactShare'></a>

`exactShare` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The share of buildings whose first detection year equals the label\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(System.Text.Json.Nodes.JsonObject)'></a>

## YOLODetectorEvaluation\(JsonObject\) Constructor

Initializes a new instance of the [YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YOLODetectorEvaluation(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.YOLODetectorEvaluation(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.Count'></a>

## YOLODetectorEvaluation\.Count Property

Gets the number of buildings in the subset\. State it beside every metric: the clean subset is much smaller than the full Test set\.

```csharp
public int Count { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.ExactShare'></a>

## YOLODetectorEvaluation\.ExactShare Property

Gets the share of buildings whose first detection year equals the label, in \[0, 1\], or 0 for an empty subset \- read it with [Count](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.Count 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation\.Count')\.

```csharp
public double ExactShare { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.MeanAbsoluteError'></a>

## YOLODetectorEvaluation\.MeanAbsoluteError Property

Gets the mean absolute difference between the first detection year and the label, in years, or 0 for an empty subset \- read it with [Count](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.Count 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation\.Count')\.

```csharp
public double MeanAbsoluteError { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.RootMeanSquareError'></a>

## YOLODetectorEvaluation\.RootMeanSquareError Property

Gets the root mean square difference between the first detection year and the label, in years, or 0 for an empty subset \- read it with [Count](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.Count 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation\.Count')\.

```csharp
public double RootMeanSquareError { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.SHA256'></a>

## YOLODetectorEvaluation\.SHA256 Property

Gets the SHA\-256 of the weights file, in lower\-case hexadecimal \- the identity of the weights, whatever the file is called\.

```csharp
public string? SHA256 { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.Subset'></a>

## YOLODetectorEvaluation\.Subset Property

Gets the subset of the Test buildings the row covers\.

```csharp
public DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset Subset { get; }
```

#### Property Value
[YOLODetectorEvaluationSubset](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset 'DiGi\.GIS\.YOLO\.UI\.Enums\.YOLODetectorEvaluationSubset')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation.WeightsPath'></a>

## YOLODetectorEvaluation\.WeightsPath Property

Gets the weights file evaluated\.

```csharp
public string? WeightsPath { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult'></a>

## YOLODetectorEvaluationResult Class

What one detector evaluation found: one row per weights file per subset of the Test buildings, all run over the same images\.

[FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult\.FailedStepNames') says whether every weights file was scored. A weights file that cannot be run is reported and stepped over, so the rows of the others still come back.

```csharp
public class YOLODetectorEvaluationResult : DiGi.Core.Classes.SerializableResult, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableresult 'DiGi\.Core\.Classes\.SerializableResult') → YOLODetectorEvaluationResult

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult)'></a>

## YOLODetectorEvaluationResult\(YOLODetectorEvaluationResult\) Constructor

Initializes a new instance of the [YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult') class by copying an existing one\.

```csharp
public YOLODetectorEvaluationResult(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult? yOLODetectorEvaluationResult);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult).yOLODetectorEvaluationResult'></a>

`yOLODetectorEvaluationResult` [YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult')

The [YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_)'></a>

## YOLODetectorEvaluationResult\(string, IEnumerable\<YOLODetectorEvaluation\>, IEnumerable\<string\>, IEnumerable\<string\>, Nullable\<DateTimeOffset\>, Nullable\<DateTimeOffset\>\) Constructor

Initializes a new instance of the [YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult') class\.

```csharp
public YOLODetectorEvaluationResult(string? outputDirectory, System.Collections.Generic.IEnumerable<DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation>? yOLODetectorEvaluations, System.Collections.Generic.IEnumerable<string>? failedStepNames, System.Collections.Generic.IEnumerable<string>? messages, System.Nullable<System.DateTimeOffset> start, System.Nullable<System.DateTimeOffset> end);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).outputDirectory'></a>

`outputDirectory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The root directory of the dataset evaluated\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).yOLODetectorEvaluations'></a>

`yOLODetectorEvaluations` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The rows, one per weights file per subset, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).failedStepNames'></a>

`failedStepNames` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The steps that reported a failure, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).messages'></a>

`messages` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

What the run has to say beyond its rows, or null for nothing\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).start'></a>

`start` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run started\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(string,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).end'></a>

`end` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run ended\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(System.Text.Json.Nodes.JsonObject)'></a>

## YOLODetectorEvaluationResult\(JsonObject\) Constructor

Initializes a new instance of the [YOLODetectorEvaluationResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluationResult') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YOLODetectorEvaluationResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluationResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.End'></a>

## YOLODetectorEvaluationResult\.End Property

Gets when the run ended\.

```csharp
public System.Nullable<System.DateTimeOffset> End { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.FailedStepNames'></a>

## YOLODetectorEvaluationResult\.FailedStepNames Property

Gets the steps that reported a failure\. Empty means every weights file was scored\.

```csharp
public System.Collections.Generic.List<string> FailedStepNames { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.Messages'></a>

## YOLODetectorEvaluationResult\.Messages Property

Gets what the run has to say beyond its rows\.

```csharp
public System.Collections.Generic.List<string> Messages { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.OutputDirectory'></a>

## YOLODetectorEvaluationResult\.OutputDirectory Property

Gets the root directory of the dataset evaluated\.

```csharp
public string? OutputDirectory { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.Start'></a>

## YOLODetectorEvaluationResult\.Start Property

Gets when the run started\.

```csharp
public System.Nullable<System.DateTimeOffset> Start { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluationResult.YOLODetectorEvaluations'></a>

## YOLODetectorEvaluationResult\.YOLODetectorEvaluations Property

Gets the rows, one per weights file per subset, in the order the weights were named\.

```csharp
public System.Collections.Generic.List<DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation> YOLODetectorEvaluations { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[YOLODetectorEvaluation](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLODetectorEvaluation 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLODetectorEvaluation')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult'></a>

## YOLOLabelCheckResult Class

How well the label boxes of a training dataset agree with what the current detector finds on the same images: the intersection over union of each label box with the best\-overlapping detection\.

The `train8` label files are lost, so the new labels cannot be compared with the old ones directly. The detector trained on them is the next best record: a low mean intersection over union means the new boxes are shifted or scaled against the ones it learned from - the device-independent-pixel normalisation of the legacy builder is the known suspect - and training should stop until it is explained.

```csharp
public class YOLOLabelCheckResult : DiGi.Core.Classes.SerializableResult, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableresult 'DiGi\.Core\.Classes\.SerializableResult') → YOLOLabelCheckResult

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult)'></a>

## YOLOLabelCheckResult\(YOLOLabelCheckResult\) Constructor

Initializes a new instance of the [YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult') class by copying an existing one\.

```csharp
public YOLOLabelCheckResult(DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult? yOLOLabelCheckResult);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult).yOLOLabelCheckResult'></a>

`yOLOLabelCheckResult` [YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult')

The [YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_)'></a>

## YOLOLabelCheckResult\(int, int, double, double, double, IDictionary\<string,double\>, string, IEnumerable\<string\>, IEnumerable\<string\>, Nullable\<DateTimeOffset\>, Nullable\<DateTimeOffset\>\) Constructor

Initializes a new instance of the [YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult') class\.

```csharp
public YOLOLabelCheckResult(int sampleCount, int detectedCount, double meanIntersectionOverUnion, double medianIntersectionOverUnion, double shareAboveHalf, System.Collections.Generic.IDictionary<string,double>? countyIntersectionOverUnions, string? overlayDirectory, System.Collections.Generic.IEnumerable<string>? failedStepNames, System.Collections.Generic.IEnumerable<string>? messages, System.Nullable<System.DateTimeOffset> start, System.Nullable<System.DateTimeOffset> end);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).sampleCount'></a>

`sampleCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of positive images checked\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).detectedCount'></a>

`detectedCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of checked images the detector found anything on\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).meanIntersectionOverUnion'></a>

`meanIntersectionOverUnion` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The mean intersection over union across the sample; an image with no detection counts as 0\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).medianIntersectionOverUnion'></a>

`medianIntersectionOverUnion` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The median intersection over union across the sample\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).shareAboveHalf'></a>

`shareAboveHalf` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The share of the sample with an intersection over union of 0\.5 or more\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).countyIntersectionOverUnions'></a>

`countyIntersectionOverUnions` [System\.Collections\.Generic\.IDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2 'System\.Collections\.Generic\.IDictionary\`2')

The mean intersection over union per county part, keyed by the county identifier in invariant culture, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).overlayDirectory'></a>

`overlayDirectory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The directory the overlay images were written to\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).failedStepNames'></a>

`failedStepNames` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The steps that reported a failure, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).messages'></a>

`messages` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

What the run has to say beyond its numbers, or null for nothing\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).start'></a>

`start` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run started\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(int,int,double,double,double,System.Collections.Generic.IDictionary_string,double_,string,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_).end'></a>

`end` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run ended\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(System.Text.Json.Nodes.JsonObject)'></a>

## YOLOLabelCheckResult\(JsonObject\) Constructor

Initializes a new instance of the [YOLOLabelCheckResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOLabelCheckResult') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YOLOLabelCheckResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.YOLOLabelCheckResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.CountyIntersectionOverUnions'></a>

## YOLOLabelCheckResult\.CountyIntersectionOverUnions Property

Gets the mean intersection over union per county part, keyed by the county identifier in invariant culture\. A county that stands out from the others points at data rather than at the rule\.

```csharp
public System.Collections.Generic.Dictionary<string,double> CountyIntersectionOverUnions { get; }
```

#### Property Value
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.DetectedCount'></a>

## YOLOLabelCheckResult\.DetectedCount Property

Gets the number of checked images the detector found anything on\.

```csharp
public int DetectedCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.End'></a>

## YOLOLabelCheckResult\.End Property

Gets when the run ended\.

```csharp
public System.Nullable<System.DateTimeOffset> End { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.FailedStepNames'></a>

## YOLOLabelCheckResult\.FailedStepNames Property

Gets the steps that reported a failure\. Empty means the check ran over the whole sample\.

```csharp
public System.Collections.Generic.List<string> FailedStepNames { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.MeanIntersectionOverUnion'></a>

## YOLOLabelCheckResult\.MeanIntersectionOverUnion Property

Gets the mean intersection over union across the sample\. An image the detector found nothing on counts as 0\.

```csharp
public double MeanIntersectionOverUnion { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.MedianIntersectionOverUnion'></a>

## YOLOLabelCheckResult\.MedianIntersectionOverUnion Property

Gets the median intersection over union across the sample\.

```csharp
public double MedianIntersectionOverUnion { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.Messages'></a>

## YOLOLabelCheckResult\.Messages Property

Gets what the run has to say beyond its numbers\.

```csharp
public System.Collections.Generic.List<string> Messages { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.OverlayDirectory'></a>

## YOLOLabelCheckResult\.OverlayDirectory Property

Gets the directory the overlay images \- the label box and the best detection drawn on the image \- were written to\.

```csharp
public string? OverlayDirectory { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.SampleCount'></a>

## YOLOLabelCheckResult\.SampleCount Property

Gets the number of positive images checked\.

```csharp
public int SampleCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.ShareAboveHalf'></a>

## YOLOLabelCheckResult\.ShareAboveHalf Property

Gets the share of the sample with an intersection over union of 0\.5 or more \- the usual threshold for a box counting as found\.

```csharp
public double ShareAboveHalf { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOLabelCheckResult.Start'></a>

## YOLOLabelCheckResult\.Start Property

Gets when the run started\.

```csharp
public System.Nullable<System.DateTimeOffset> Start { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount'></a>

## YOLOTrainingDatasetCount Class

The tallies of a YOLO training dataset build, for one county part or for the whole run\.

The counting half - labelled buildings, the split, the Legacy agreement table, bounded entries, cross-part duplicates and the estimates - is filled by a `CountOnly` run as well as by a build; the building half - images, boxes, skipped and failed buildings - only by a build.

The Legacy counts cover the Test buildings only: the clean subset of the detector evaluation is Test and not Legacy, and a Train or Validate building's Legacy decision feeds no metric.

```csharp
public class YOLOTrainingDatasetCount : DiGi.Core.Classes.SerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → YOLOTrainingDatasetCount

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount)'></a>

## YOLOTrainingDatasetCount\(YOLOTrainingDatasetCount\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount') class by copying an existing one\.

```csharp
public YOLOTrainingDatasetCount(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount? yOLOTrainingDatasetCount);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount).yOLOTrainingDatasetCount'></a>

`yOLOTrainingDatasetCount` [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')

The [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(System.Nullable_int_)'></a>

## YOLOTrainingDatasetCount\(Nullable\<int\>\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount') class with every tally at zero\.

```csharp
public YOLOTrainingDatasetCount(System.Nullable<int> countyId);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(System.Nullable_int_).countyId'></a>

`countyId` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The identifier of the county part the tallies cover, or null for the whole run\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(System.Text.Json.Nodes.JsonObject)'></a>

## YOLOTrainingDatasetCount\(JsonObject\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YOLOTrainingDatasetCount(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.YOLOTrainingDatasetCount(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.BoundedEntryCount'></a>

## YOLOTrainingDatasetCount\.BoundedEntryCount Property

Gets or sets the number of Test buildings carrying a bounded user entry \(at or before, or after a year\)\.

Bounded entries are not trained on; the count lets a later cycle decide whether to use them.

```csharp
public long BoundedEntryCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.BuildingCount'></a>

## YOLOTrainingDatasetCount\.BuildingCount Property

Gets or sets the number of buildings the dataset takes from the county part, after de\-duplication\.

```csharp
public long BuildingCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.ClampedBoxCount'></a>

## YOLOTrainingDatasetCount\.ClampedBoxCount Property

Gets or sets the number of boxes that crossed an image edge and were clamped to it\.

```csharp
public long ClampedBoxCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.CountyId'></a>

## YOLOTrainingDatasetCount\.CountyId Property

Gets or sets the identifier of the county part the tallies cover, or null for the whole run\.

```csharp
public System.Nullable<int> CountyId { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.DroppedBoxCount'></a>

## YOLOTrainingDatasetCount\.DroppedBoxCount Property

Gets or sets the number of boxes with no area inside their image\.

The image is then not written at all: a positive year with no box would teach the detector the building is absent.

```csharp
public long DroppedBoxCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.DuplicateReferenceCount'></a>

## YOLOTrainingDatasetCount\.DuplicateReferenceCount Property

Gets or sets the number of labelled references dropped because a lower\-numbered county part of the run already holds them\.

A reference is unique only per county part, so one filed under two named parts would otherwise be built twice - and could land in two splits.

```csharp
public long DuplicateReferenceCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.EstimatedByteCount'></a>

## YOLOTrainingDatasetCount\.EstimatedByteCount Property

Gets or sets the estimated disk space, in bytes, a build would take, from an assumed size per image\.

```csharp
public long EstimatedByteCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.EstimatedImageCount'></a>

## YOLOTrainingDatasetCount\.EstimatedImageCount Property

Gets or sets the estimated number of images a build would write, from an assumed number of orthophoto years per building\.

```csharp
public long EstimatedImageCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.EstimatedRequestCount'></a>

## YOLOTrainingDatasetCount\.EstimatedRequestCount Property

Gets or sets the estimated number of Web API requests a build would make: one orthophoto read per building, plus the paged footprint reads\.

```csharp
public long EstimatedRequestCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.FailedBuildingCount'></a>

## YOLOTrainingDatasetCount\.FailedBuildingCount Property

Gets or sets the number of buildings that failed while their images were saved\.

Each is logged and stepped over.

```csharp
public long FailedBuildingCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.IdenticalImageDroppedCount'></a>

## YOLOTrainingDatasetCount\.IdenticalImageDroppedCount Property

Gets or sets the number of images dropped because the same photo bytes appear under another year of the building with a conflicting label \- one year positive, the other negative\.

Identical pixels with contradictory labels teach nothing but noise.

```csharp
public long IdenticalImageDroppedCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.IdenticalImageMergedCount'></a>

## YOLOTrainingDatasetCount\.IdenticalImageMergedCount Property

Gets or sets the number of images dropped because the same photo bytes appear under an earlier year of the building with an agreeing label\.

The earliest is kept.

```csharp
public long IdenticalImageMergedCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.ImageCount'></a>

## YOLOTrainingDatasetCount\.ImageCount Property

Gets or sets the number of images written\.

```csharp
public long ImageCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LabelConflictCount'></a>

## YOLOTrainingDatasetCount\.LabelConflictCount Property

Gets or sets the number of dropped duplicate references whose label differs between the parts\.

The label of the lowest-numbered part is kept.

```csharp
public long LabelConflictCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LabelledCount'></a>

## YOLOTrainingDatasetCount\.LabelledCount Property

Gets or sets the number of references the county part holds a label for \- an exact user year built in the building data\.

```csharp
public long LabelledCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LegacyBothCount'></a>

## YOLOTrainingDatasetCount\.LegacyBothCount Property

Gets or sets the number of Test buildings both records name\.

```csharp
public long LegacyBothCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LegacyNoneCount'></a>

## YOLOTrainingDatasetCount\.LegacyNoneCount Property

Gets or sets the number of Test buildings neither record names \- the clean subset\.

```csharp
public long LegacyNoneCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LegacyTimestampCount'></a>

## YOLOTrainingDatasetCount\.LegacyTimestampCount Property

Gets or sets the number of Test buildings only the stored history names \- a user entry undated or dated before the cut\-off\.

```csharp
public long LegacyTimestampCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LegacyTsvCount'></a>

## YOLOTrainingDatasetCount\.LegacyTsvCount Property

Gets or sets the number of Test buildings only the legacy reference list names\.

```csharp
public long LegacyTsvCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.LegacyUnknownCount'></a>

## YOLOTrainingDatasetCount\.LegacyUnknownCount Property

Gets or sets the number of Test buildings whose history could not be read\.

They are Legacy, never clean.

```csharp
public long LegacyUnknownCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.NegativeImageCount'></a>

## YOLOTrainingDatasetCount\.NegativeImageCount Property

Gets or sets the number of images of a year before the label \- registered with an empty label file, as background\.

```csharp
public long NegativeImageCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.PositiveImageCount'></a>

## YOLOTrainingDatasetCount\.PositiveImageCount Property

Gets or sets the number of images of a year at or after the label \- each carries the building's box\.

```csharp
public long PositiveImageCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.ReferenceDuplicateCount'></a>

## YOLOTrainingDatasetCount\.ReferenceDuplicateCount Property

Gets or sets the number of references the API reports as filed under several county parts, among them this one\.

```csharp
public long ReferenceDuplicateCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.ResumedCount'></a>

## YOLOTrainingDatasetCount\.ResumedCount Property

Gets or sets the number of buildings skipped because the manifest already records them as complete\.

```csharp
public long ResumedCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.SameYearImageCount'></a>

## YOLOTrainingDatasetCount\.SameYearImageCount Property

Gets or sets the number of orthophotos skipped because an earlier one of the same year was already taken\.

One photo per year.

```csharp
public long SameYearImageCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.TestCount'></a>

## YOLOTrainingDatasetCount\.TestCount Property

Gets or sets the number of buildings in the Test split \- the held\-out ones\.

```csharp
public long TestCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.TrainCount'></a>

## YOLOTrainingDatasetCount\.TrainCount Property

Gets or sets the number of buildings in the Train split\.

```csharp
public long TrainCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.ValidateCount'></a>

## YOLOTrainingDatasetCount\.ValidateCount Property

Gets or sets the number of buildings in the Validate split\.

```csharp
public long ValidateCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.WithoutFootprintCount'></a>

## YOLOTrainingDatasetCount\.WithoutFootprintCount Property

Gets or sets the number of buildings skipped because no footprint could be read\.

```csharp
public long WithoutFootprintCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount.WithoutImageryCount'></a>

## YOLOTrainingDatasetCount\.WithoutImageryCount Property

Gets or sets the number of buildings skipped because no orthophoto could be read\.

```csharp
public long WithoutImageryCount { get; set; }
```

#### Property Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions'></a>

## YOLOTrainingDatasetOptions Class

Provides the settings of the YOLO training dataset tooling: which counties the dataset is built from and where it goes, how buildings are labelled and split, and what the label check and the detector evaluation run with\.

One options type serves the three console modes - `--dataset`, `--check-labels` and `--evaluate-detector` - because the two checks read the dataset the first one built, and one file then names it once.

Every default is a working value except the two that state scope: [CountyIds](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.CountyIds 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.CountyIds') and [OutputDirectory](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.OutputDirectory 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.OutputDirectory') have none, so a dataset is never built from counties nobody named into a folder nobody chose. There is deliberately no member for the Web API key; it travels on [DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager\.Key](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.webapi.classes.giswebapimanager.key 'DiGi\.GIS\.WebAPI\.Classes\.GISWebAPIManager\.Key').

```csharp
public class YOLOTrainingDatasetOptions : DiGi.Core.Classes.SerializableOptions, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → YOLOTrainingDatasetOptions

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.YOLOTrainingDatasetOptions()'></a>

## YOLOTrainingDatasetOptions\(\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions') class with default values\.

```csharp
public YOLOTrainingDatasetOptions();
```

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.YOLOTrainingDatasetOptions(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions)'></a>

## YOLOTrainingDatasetOptions\(YOLOTrainingDatasetOptions\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions') class by copying an existing options instance\.

```csharp
public YOLOTrainingDatasetOptions(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions? yOLOTrainingDatasetOptions);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.YOLOTrainingDatasetOptions(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions).yOLOTrainingDatasetOptions'></a>

`yOLOTrainingDatasetOptions` [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions')

The source options instance to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.YOLOTrainingDatasetOptions(System.Text.Json.Nodes.JsonObject)'></a>

## YOLOTrainingDatasetOptions\(JsonObject\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions') class using a JSON object\.

```csharp
public YOLOTrainingDatasetOptions(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.YOLOTrainingDatasetOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the configuration settings\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Confidence'></a>

## YOLOTrainingDatasetOptions\.Confidence Property

Gets or sets the confidence threshold a detection has to reach to be reported by the label check and the detector evaluation, passed to the prediction script as \-\-conf\.

The default is the production threshold, so the evaluation measures the detector as the pipeline runs it.

```csharp
public double Confidence { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.CountOnly'></a>

## YOLOTrainingDatasetOptions\.CountOnly Property

Gets or sets whether `--dataset` only counts: per county, the labelled buildings, the split, the Legacy agreement table, bounded entries, cross\-part duplicates and an estimate of the requests, images and disk a build would need\.

No orthophoto is requested and nothing is written. Run it first and post the output on the tracking issue before a build.

```csharp
public bool CountOnly { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.CountyIds'></a>

## YOLOTrainingDatasetOptions\.CountyIds Property

Gets or sets the county rows the dataset is built from, by identifier\.

Identifiers rather than codes, and each identifier is a polygon part: name every part of a county. A reference found under several named parts is built once, under the lowest identifier.

There is no default. The scope is always stated.

```csharp
public System.Collections.Generic.HashSet<int>? CountyIds { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.HoldoutDenominator'></a>

## YOLOTrainingDatasetOptions\.HoldoutDenominator Property

Gets or sets the holdout rate as one in how many references: a reference is held out when [DiGi\.GIS\.IO\.Query\.Holdout\(System\.String,System\.Int32\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.query.holdout#digi-gis-io-query-holdout(system-string-system-int32) 'DiGi\.GIS\.IO\.Query\.Holdout\(System\.String,System\.Int32\)') says so, and a held\-out reference goes to the Test split only\.

The default, 5, is the holdout the Year Built regressor uses, so both models are measured on the same buildings. Change it only together with the regressor.

```csharp
public int HoldoutDenominator { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.LabelCheckOverlayCount'></a>

## YOLOTrainingDatasetOptions\.LabelCheckOverlayCount Property

Gets or sets how many label check samples are written as overlay images \- the label box and the best detection drawn on the image \- to the reports folder\.

```csharp
public int LabelCheckOverlayCount { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.LabelCheckSampleSize'></a>

## YOLOTrainingDatasetOptions\.LabelCheckSampleSize Property

Gets or sets how many positive images the label check runs the detector over, drawn with [Seed](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Seed 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.Seed') from the Train and Validate splits\.

```csharp
public int LabelCheckSampleSize { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.LegacyCutoff'></a>

## YOLOTrainingDatasetOptions\.LegacyCutoff Property

Gets or sets the moment the `train8` detector was saved\. A held\-out building with a user entry that is undated or dated before it may have been seen by `train8`, and is Legacy\.

The default, 2025-05-23T00:00:00Z, is `train8`'s save date from the DiGi.YOLO provenance table.

```csharp
public System.DateTimeOffset LegacyCutoff { get; set; }
```

#### Property Value
[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.LegacyReferencesFilePath'></a>

## YOLOTrainingDatasetOptions\.LegacyReferencesFilePath Property

Gets or sets the path of the legacy reference list \- the regressor training table `Data_2025.05.27.tsv` kept in DiGi\.GIS\.ML under `Data/` \- resolved like [ModelPath](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ModelPath 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.ModelPath')\.

A reference it names is Legacy: the table was built from the same sources days after `train8`. Copy it into the git-ignored `user files` folder; the default names it there. The build is refused when it cannot be read, because a missing list would report every held-out building as clean.

```csharp
public string? LegacyReferencesFilePath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.MaxConcurrentRequests'></a>

## YOLOTrainingDatasetOptions\.MaxConcurrentRequests Property

Gets or sets how many orthophoto requests may be in flight at once\.

The deployed API shares one connection pool across every caller, so keep this to a handful.

```csharp
public int MaxConcurrentRequests { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ModelPath'></a>

## YOLOTrainingDatasetOptions\.ModelPath Property

Gets or sets the path of the weights the label check runs, resolved against the runner by [ModelPath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ModelPath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ModelPath\(string\)')\.

The default is the production detector, `train8`: the check compares the new labels with what the current detector finds.

```csharp
public string? ModelPath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Offset'></a>

## YOLOTrainingDatasetOptions\.Offset Property

Gets or sets the distance, in metres, a building's bounding box is grown by on every side before it is projected onto an orthophoto\.

The default, 1, is the offset the legacy builder used for `train8`.

```csharp
public double Offset { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.OutputDirectory'></a>

## YOLOTrainingDatasetOptions\.OutputDirectory Property

Gets or sets the root directory of the dataset: `conf.yaml`, the `images` and `labels` folders, and the `dataset_references.tsv` manifest\.

Must be absolute - it is written into `conf.yaml` as the dataset path. There is no default.

```csharp
public string? OutputDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.PythonPath'></a>

## YOLOTrainingDatasetOptions\.PythonPath Property

Gets or sets the path of the CPython interpreter that runs the prediction script, or the name of one on PATH\. Used by the label check and the detector evaluation\.

```csharp
public string? PythonPath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ReferenceBatchSize'></a>

## YOLOTrainingDatasetOptions\.ReferenceBatchSize Property

Gets or sets how many references a bulk read is asked for in one request\. A larger value is clamped to the endpoint cap of ten thousand\.

```csharp
public int ReferenceBatchSize { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ReferenceDuplicateLimit'></a>

## YOLOTrainingDatasetOptions\.ReferenceDuplicateLimit Property

Gets or sets the most cross\-part reference duplicates `CountOnly` asks the API for\. The endpoint is global \- it cannot be filtered by county \- so it is read once and filtered here\.

```csharp
public int ReferenceDuplicateLimit { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ReportsDirectory'></a>

## YOLOTrainingDatasetOptions\.ReportsDirectory Property

Gets or sets the directory the label check writes its overlay images to\. A relative path is resolved against the current directory\.

Generated output, so the default is the git-ignored `user files/reports` folder, never `files`.

```csharp
public string? ReportsDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Resume'></a>

## YOLOTrainingDatasetOptions\.Resume Property

Gets or sets whether a build into an existing dataset continues it rather than refusing\.

The manifest is the journal: a building is appended to it once all of its images and label files are written, a building already in it is skipped, and the files of a building that is not in it - one a stopped run left half-written - are removed and rebuilt. With it off, an existing dataset is refused.

```csharp
public bool Resume { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Seed'></a>

## YOLOTrainingDatasetOptions\.Seed Property

Gets or sets the seed of the Train / Validate split and of the label check sample\.

The split is drawn over the non-holdout references sorted ordinally, so the same labels and seed always give the same split.

```csharp
public int Seed { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.ValidateWeight'></a>

## YOLOTrainingDatasetOptions\.ValidateWeight Property

Gets or sets the share of the non\-holdout references that go to the Validate split; the rest go to Train\.

```csharp
public double ValidateWeight { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.WeightsPaths'></a>

## YOLOTrainingDatasetOptions\.WeightsPaths Property

Gets or sets the weights files the detector evaluation compares, each resolved by [ModelPath\(string\)](DiGi.GIS.YOLO.UI.md#DiGi.GIS.YOLO.UI.Query.ModelPath(string) 'DiGi\.GIS\.YOLO\.UI\.Query\.ModelPath\(string\)')\.

Every file is run over the same Test images and reported as one row per subset, so list the production weights alongside the candidates.

```csharp
public System.Collections.Generic.List<string>? WeightsPaths { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.WorkingDirectory'></a>

## YOLOTrainingDatasetOptions\.WorkingDirectory Property

Gets or sets the directory the prediction process runs in, which is also where the runner keeps the Python scripts\. Null uses the folder of the prediction output\.

```csharp
public string? WorkingDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetOptions.Years'></a>

## YOLOTrainingDatasetOptions\.Years Property

Gets or sets the range of years the detector evaluation reads first detection years over\. Null uses the default of [DiGi\.GIS\.IO\.Query\.FirstDetectionYears\(DiGi\.Core\.IO\.Table\.Classes\.Table,DiGi\.Core\.Classes\.Range\{System\.Int32\},System\.Int16\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.io.query.firstdetectionyears#digi-gis-io-query-firstdetectionyears(digi-core-io-table-classes-table-digi-core-classes-range{system-int32}-system-int16) 'DiGi\.GIS\.IO\.Query\.FirstDetectionYears\(DiGi\.Core\.IO\.Table\.Classes\.Table,DiGi\.Core\.Classes\.Range\{System\.Int32\},System\.Int16\)'), 2008 to 2025\.

```csharp
public DiGi.Core.Classes.Range<int>? Years { get; set; }
```

#### Property Value
[DiGi\.Core\.Classes\.Range&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult'></a>

## YOLOTrainingDatasetResult Class

What one YOLO training dataset build \- or `CountOnly` run \- did: the tallies per county part and in total, and what it could not finish\.

[FailedStepNames](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.FailedStepNames 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult\.FailedStepNames') is what says whether the run did everything it set out to do. A building that fails is logged and stepped over, and a county part whose labels cannot be read is refused while the others are built, so a result that came back at all is not by itself evidence of a complete dataset.

```csharp
public class YOLOTrainingDatasetResult : DiGi.Core.Classes.SerializableResult, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject, DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableResult](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableresult 'DiGi\.Core\.Classes\.SerializableResult') → YOLOTrainingDatasetResult

Implements [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject'), [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult)'></a>

## YOLOTrainingDatasetResult\(YOLOTrainingDatasetResult\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult') class by copying an existing one\.

```csharp
public YOLOTrainingDatasetResult(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult? yOLOTrainingDatasetResult);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult).yOLOTrainingDatasetResult'></a>

`yOLOTrainingDatasetResult` [YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult')

The [YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult') to copy from\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool)'></a>

## YOLOTrainingDatasetResult\(IEnumerable\<int\>, string, bool, YOLOTrainingDatasetCount, IEnumerable\<YOLOTrainingDatasetCount\>, IEnumerable\<string\>, IEnumerable\<string\>, Nullable\<DateTimeOffset\>, Nullable\<DateTimeOffset\>, bool\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult') class\.

```csharp
public YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable<int>? countyIds, string? outputDirectory, bool countOnly, DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount? total, System.Collections.Generic.IEnumerable<DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount>? yOLOTrainingDatasetCounts, System.Collections.Generic.IEnumerable<string>? failedStepNames, System.Collections.Generic.IEnumerable<string>? messages, System.Nullable<System.DateTimeOffset> start, System.Nullable<System.DateTimeOffset> end, bool cancelled);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).countyIds'></a>

`countyIds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The county parts the run covered, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).outputDirectory'></a>

`outputDirectory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The root directory of the dataset\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).countOnly'></a>

`countOnly` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether the run only counted\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).total'></a>

`total` [YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')

The tallies of the whole run\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).yOLOTrainingDatasetCounts'></a>

`yOLOTrainingDatasetCounts` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The tallies per county part, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).failedStepNames'></a>

`failedStepNames` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The steps that reported a failure, or null for none\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).messages'></a>

`messages` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

What the run has to say beyond its tallies, or null for nothing\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).start'></a>

`start` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run started\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).end'></a>

`end` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

When the run ended\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Collections.Generic.IEnumerable_int_,string,bool,DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount,System.Collections.Generic.IEnumerable_DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount_,System.Collections.Generic.IEnumerable_string_,System.Collections.Generic.IEnumerable_string_,System.Nullable_System.DateTimeOffset_,System.Nullable_System.DateTimeOffset_,bool).cancelled'></a>

`cancelled` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Whether the run was stopped before it covered everything it was given\.

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Text.Json.Nodes.JsonObject)'></a>

## YOLOTrainingDatasetResult\(JsonObject\) Constructor

Initializes a new instance of the [YOLOTrainingDatasetResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult') class from a [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public YOLOTrainingDatasetResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the serialized data\.
### Properties

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.Cancelled'></a>

## YOLOTrainingDatasetResult\.Cancelled Property

Gets whether the run was stopped before it covered everything it was given\. What it wrote is written, and the manifest lets a re\-run continue\.

```csharp
public bool Cancelled { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.CountOnly'></a>

## YOLOTrainingDatasetResult\.CountOnly Property

Gets whether the run only counted\. A counting run requests no orthophoto and writes nothing\.

```csharp
public bool CountOnly { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.CountyIds'></a>

## YOLOTrainingDatasetResult\.CountyIds Property

Gets the county parts the run covered, by identifier\.

```csharp
public System.Collections.Generic.List<int> CountyIds { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.Duration'></a>

## YOLOTrainingDatasetResult\.Duration Property

Gets how long the run took, or null when either end of it is unknown\.

```csharp
public System.Nullable<System.TimeSpan> Duration { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.End'></a>

## YOLOTrainingDatasetResult\.End Property

Gets when the run ended\.

```csharp
public System.Nullable<System.DateTimeOffset> End { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.FailedStepNames'></a>

## YOLOTrainingDatasetResult\.FailedStepNames Property

Gets the steps that reported a failure\. Empty means the run did everything it set out to do\.

```csharp
public System.Collections.Generic.List<string> FailedStepNames { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.Messages'></a>

## YOLOTrainingDatasetResult\.Messages Property

Gets what the run has to say beyond its tallies\.

```csharp
public System.Collections.Generic.List<string> Messages { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.OutputDirectory'></a>

## YOLOTrainingDatasetResult\.OutputDirectory Property

Gets the root directory of the dataset\.

```csharp
public string? OutputDirectory { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.Start'></a>

## YOLOTrainingDatasetResult\.Start Property

Gets when the run started\.

```csharp
public System.Nullable<System.DateTimeOffset> Start { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.Total'></a>

## YOLOTrainingDatasetResult\.Total Property

Gets the tallies of the whole run \- the sum of [YOLOTrainingDatasetCounts](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetCounts 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetResult\.YOLOTrainingDatasetCounts')\.

```csharp
public DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount? Total { get; }
```

#### Property Value
[YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')

<a name='DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetResult.YOLOTrainingDatasetCounts'></a>

## YOLOTrainingDatasetResult\.YOLOTrainingDatasetCounts Property

Gets the tallies per county part, in ascending identifier order\.

```csharp
public System.Collections.Generic.List<DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount> YOLOTrainingDatasetCounts { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[YOLOTrainingDatasetCount](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YOLOTrainingDatasetCount 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetCount')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')