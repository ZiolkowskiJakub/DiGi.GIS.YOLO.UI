#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Enums Namespace
### Enums

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode'></a>

## YearBuiltPredictionExitCode Enum

Names what the exit code of the headless Year Built prediction runner means\.

The runner is started by other processes - the tray application's background task among them - and an exit code is the whole of what they get back. Naming the codes here rather than writing integers on both sides is what keeps a caller's reading of a run and the runner's own verdict from drifting apart: a caller comparing against a literal cannot fail to compile when a code changes meaning.

Anything other than [Succeeded](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Succeeded 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Succeeded') means no run finished. [Cancelled](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Cancelled 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Cancelled') is not a failure of the pipeline, and [Environment](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Environment 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Environment') is deliberately separate from [Failed](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Failed 'DiGi\.GIS\.YOLO\.UI\.Enums\.YearBuiltPredictionExitCode\.Failed') - a machine that cannot start the detector at all is a different thing to fix than a step that failed while running.

```csharp
public enum YearBuiltPredictionExitCode
```
### Fields

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Succeeded'></a>

`Succeeded` 0

The pipeline ran and every step it was asked for completed\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Configuration'></a>

`Configuration` 1

The options could not be loaded, or they name no county or no scratch directory\. Nothing was attempted\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Environment'></a>

`Environment` 2

The preflight found that this machine cannot run the detector \- no CPython carrying ultralytics, or no weights\. Nothing was exported\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Authorization'></a>

`Authorization` 3

The Web API authorization key is missing, or the client could not be built from it\. Nothing was read or written\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Failed'></a>

`Failed` 4

The run started and one or more of its steps did not complete\. What it managed to write is written\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Cancelled'></a>

`Cancelled` 5

The run was stopped before it finished\. What it had already written is committed\.