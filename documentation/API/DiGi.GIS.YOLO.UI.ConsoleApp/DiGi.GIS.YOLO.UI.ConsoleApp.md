#### [DiGi\.GIS\.YOLO\.UI\.ConsoleApp](DiGi.GIS.YOLO.UI.ConsoleApp.Overview.md 'DiGi\.GIS\.YOLO\.UI\.ConsoleApp\.Overview')

## DiGi\.GIS\.YOLO\.UI\.ConsoleApp Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program'></a>

## Program Class

Provides the main entry point for the headless YOLO Year Built prediction runner\.

```csharp
public static class Program
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Program
### Fields

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.Usage'></a>

## Program\.Usage Field

The usage text printed when the arguments or the options cannot be read\.

```csharp
private const string Usage = "Usage: DiGi.GIS.YOLO.UI.ConsoleApp [path-to-options.json]
       DiGi.GIS.YOLO.UI.ConsoleApp --dataset|--check-labels|--evaluate-detector [path-to-YOLOTrainingDatasetOptions.json]
       DiGi.GIS.YOLO.UI.ConsoleApp --train [path-to-YOLOTrainingRunOptions.json]";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')
### Methods

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.DatasetModeAsync(string,string)'></a>

## Program\.DatasetModeAsync\(string, string\) Method

Runs one of the YOLO training dataset modes: `--dataset` builds \(or, with [DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.CountOnly](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.classes.yolotrainingdatasetoptions.countonly 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions\.CountOnly'), only counts\) a training dataset from the deployed data, `--check-labels` checks its label boxes against the current detector, and `--evaluate-detector` compares weights files on its Test buildings\.

The exit codes are the prediction run's: [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Configuration](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.configuration 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Configuration') for options that cannot be used, [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Environment](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.environment 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Environment') for weights or an interpreter the machine does not have, [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Authorization](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.authorization 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Authorization') for a missing key (`--dataset` only - the other two read nothing from the Web API), [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Failed](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.failed 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Failed') for a step that failed while running, and [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Cancelled](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.cancelled 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Cancelled').

```csharp
private static System.Threading.Tasks.Task<int> DatasetModeAsync(string mode, string? path_Options);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.DatasetModeAsync(string,string).mode'></a>

`mode` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The mode flag\.

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.DatasetModeAsync(string,string).path_Options'></a>

`path_Options` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path of the options file, or null for [DiGi\.GIS\.YOLO\.UI\.Constants\.FileName\.YOLOTrainingDatasetOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.constants.filename.yolotrainingdatasetoptions 'DiGi\.GIS\.YOLO\.UI\.Constants\.FileName\.YOLOTrainingDatasetOptions') beside the executable\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
One of [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode') as an integer\.

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.Main(string[])'></a>

## Program\.Main\(string\[\]\) Method

Executes the headless Year Built prediction pipeline from command\-line arguments\.

```csharp
public static System.Threading.Tasks.Task<int> Main(string[] args);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.Main(string[]).args'></a>

`args` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

Optional arguments\. With no flag, the first argument is the path of the prediction options file\. A leading `--dataset`, `--check-labels` or `--evaluate-detector` selects a training dataset mode instead, and the argument after it is the path of the [DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.classes.yolotrainingdatasetoptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingDatasetOptions') file\. A leading `--train` runs the whole retraining, and the argument after it is the path of the [DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.classes.yolotrainingrunoptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunOptions') file\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
One of [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode') as an integer\. Only [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Succeeded](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode.succeeded 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Succeeded') means a run finished; the rest say why one did not, and a caller reads them through that enumeration rather than against literals of its own\.

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.TrainModeAsync(string)'></a>

## Program\.TrainModeAsync\(string\) Method

Runs the `--train` mode: the dataset build, the label check, the training, the validation on the Test split and the detector evaluation as one run that stops at the first failed step\.

The start weights and the output weights are reported with their SHA-256 as soon as each is known, and the identities and the evaluation rows are printed again at the end, so the table that gates a candidate names exactly which file each row is. The exit codes are [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode'), mapped by [DiGi\.GIS\.YOLO\.UI\.Query\.YOLOTrainingRunExitCode\(DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult\)](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.query.yolotrainingrunexitcode#digi-gis-yolo-ui-query-yolotrainingrunexitcode(digi-gis-yolo-ui-classes-yolotrainingrunresult) 'DiGi\.GIS\.YOLO\.UI\.Query\.YOLOTrainingRunExitCode\(DiGi\.GIS\.YOLO\.UI\.Classes\.YOLOTrainingRunResult\)').

```csharp
private static System.Threading.Tasks.Task<int> TrainModeAsync(string? path_Options);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.TrainModeAsync(string).path_Options'></a>

`path_Options` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path of the options file, or null for [DiGi\.GIS\.YOLO\.UI\.Constants\.FileName\.YOLOTrainingRunOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.constants.filename.yolotrainingrunoptions 'DiGi\.GIS\.YOLO\.UI\.Constants\.FileName\.YOLOTrainingRunOptions') beside the executable\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
One of [DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.yolo.ui.enums.yearbuiltpredictionexitcode 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode') as an integer\.