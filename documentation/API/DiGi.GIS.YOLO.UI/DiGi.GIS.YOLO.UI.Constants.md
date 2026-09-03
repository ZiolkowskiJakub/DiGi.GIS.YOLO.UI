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

<a name='DiGi.GIS.YOLO.UI.Constants.FileName'></a>

## FileName Class

Provides constant values for configuration file names used within the GIS YOLO UI\.

```csharp
public static class FileName
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → FileName
### Fields

<a name='DiGi.GIS.YOLO.UI.Constants.FileName.GISWebAPIClientConfigurationFile'></a>

## FileName\.GISWebAPIClientConfigurationFile Field

Gets the default filename of the configuration file for the Web API client\.

```csharp
public const string GISWebAPIClientConfigurationFile = "GIS_WebAPI_Client.conf";
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