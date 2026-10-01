#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Constants Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.Constants.Count'></a>

## Count Class

Provides constant counts and limits observed by the GIS YOLO UI\.

```csharp
public static class Count
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Count
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.Count.BuildingDataReference_Maximum'></a>

## Count\.BuildingDataReference\_Maximum Field

Gets the largest number of references the building data table endpoint accepts in one request\.

Mirrors the cap the endpoint enforces. A county is thirty to a hundred and fifty thousand buildings, so a feature read is always paged; asking for more than this fails the whole request rather than merely being slower.

```csharp
public const int BuildingDataReference_Maximum = 10000;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Constants.Count.ImageByte_Estimate'></a>

## Count\.ImageByte\_Estimate Field

Gets the size, in bytes, one saved training image is assumed to take when a training dataset build is estimated before any imagery is read\.

A rough figure from the same sample building, whose orthophoto crops are ten to eighteen kilobytes each, plus a label file.

```csharp
public const long ImageByte_Estimate = 16000;
```

#### Field Value
[System\.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 'System\.Int64')

<a name='DiGi.GIS.YOLO.UI.Constants.Count.ImagePerBuilding_Estimate'></a>

## Count\.ImagePerBuilding\_Estimate Field

Gets the number of orthophoto years a building is assumed to carry when a training dataset build is estimated before any imagery is read\.

A rough figure from a sample building with eight years of coverage (2008 to 2023). The real count is only known once the imagery is read, so the estimate is an order of magnitude, not a budget.

```csharp
public const int ImagePerBuilding_Estimate = 8;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Constants.Count.YearBuiltDataReference_Maximum'></a>

## Count\.YearBuiltDataReference\_Maximum Field

Gets the largest number of references the year built data endpoint accepts in one request\.

Mirrors the cap the endpoint enforces. A county is thirty to a hundred and fifty thousand buildings, so the read is always paged; asking for more than this fails the whole request rather than merely being slower.

```csharp
public const int YearBuiltDataReference_Maximum = 10000;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.YOLO.UI.Constants.DirectoryName'></a>

## DirectoryName Class

Provides constant directory names used within the GIS YOLO UI\.

```csharp
public static class DirectoryName
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → DirectoryName
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.DirectoryName.PredictionImages'></a>

## DirectoryName\.PredictionImages Field

Gets the name of the folder a county's exported orthophoto prediction images are written to\.

```csharp
public const string PredictionImages = "images";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.DirectoryName.Weights'></a>

## DirectoryName\.Weights Field

Gets the name of the folder inside a training run that ultralytics writes the checkpoints it can resume from into\.

Holds `last.pt`, the checkpoint a resume continues, and the per-epoch checkpoints a run with `save_period` leaves behind.

```csharp
public const string Weights = "weights";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName'></a>

## FileName Class

Provides constant values for configuration file names used within the GIS YOLO UI\.

```csharp
public static class FileName
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → FileName
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.DatasetReferences'></a>

## FileName\.DatasetReferences Field

Gets the name of the manifest written beside a training dataset's conf\.yaml: one row per building with its county, split, label and Legacy decision\.

It is also the resume journal - a building is appended once all of its images and label files are written, so a building it names is complete.

```csharp
public const string DatasetReferences = "dataset_references.tsv";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.GISWebAPIClientConfigurationFile'></a>

## FileName\.GISWebAPIClientConfigurationFile Field

Gets the default filename of the configuration file for the Web API client\.

```csharp
public const string GISWebAPIClientConfigurationFile = "GIS_WebAPI_Client.conf";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.LastWeights'></a>

## FileName\.LastWeights Field

Gets the name of the checkpoint ultralytics writes into a run's `weights` folder and keeps up to date after every epoch\.

A resume continues this file, so a run whose folder holds it and no completed `<RunName>.pt` is an interrupted run.

```csharp
public const string LastWeights = "last.pt";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.PredictionResults'></a>

## FileName\.PredictionResults Field

Gets the name of the file a county's year built detections are written to by the prediction script\.

```csharp
public const string PredictionResults = "results.bbrf";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

### Remarks
The script opens it for writing rather than appending, so a repeated run over one county replaces the previous answer instead of doubling it\.

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.YearBuiltPredictionPipelineOptions'></a>

## FileName\.YearBuiltPredictionPipelineOptions Field

Gets the default filename of the configuration file for the Year Built prediction pipeline options\.

```csharp
public const string YearBuiltPredictionPipelineOptions = "YearBuiltPredictionPipelineOptions.json";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.YOLOTrainingDatasetOptions'></a>

## FileName\.YOLOTrainingDatasetOptions Field

Gets the default filename of the configuration file for the YOLO training dataset tooling \- the dataset builder, the label check and the detector evaluation\.

```csharp
public const string YOLOTrainingDatasetOptions = "YOLOTrainingDatasetOptions.json";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.YOLOTrainingRunOptions'></a>

## FileName\.YOLOTrainingRunOptions Field

Gets the default filename of the configuration file for the `--train` console mode\.

```csharp
public const string YOLOTrainingRunOptions = "YOLOTrainingRunOptions.json";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.Header'></a>

## Header Class

Provides the header lines of the tab\-separated files the GIS YOLO UI writes\.

```csharp
public static class Header
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Header
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.Header.DatasetReferences'></a>

## Header\.DatasetReferences Field

Gets the header of the `dataset_references.tsv` manifest\. The columns are read by name, so this is the contract between the dataset builder and the two checks that read the dataset\.

```csharp
public const string DatasetReferences = "Reference	CountyId	Category	Label	Legacy	LegacySource";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.LabelName'></a>

## LabelName Class

Provides the class names of the YOLO training dataset\.

```csharp
public static class LabelName
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → LabelName
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.LabelName.Building'></a>

## LabelName\.Building Field

Gets the name of the single class the year built detector is trained on\. It is added first, so it is class index 0 \- the index the production weights report\.

```csharp
public const string Building = "Building";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.YOLO.UI.Constants.MessagePrefix'></a>

## MessagePrefix Class

Provides the prefixes the headless runner marks its console output with\.

The runner's standard output is a contract when it is driven by another process rather than read by a person, and a prefix written as a literal on both sides of the pipe is a contract nothing checks. Naming them here is what lets a change to one of them fail to compile instead of quietly costing a caller its progress reporting.

```csharp
public static class MessagePrefix
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → MessagePrefix
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.MessagePrefix.Progress'></a>

## MessagePrefix\.Progress Field

Gets the prefix of the line reporting how many items a run has carried through a step\.

```csharp
public const string Progress = "[PROGRESS]";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')