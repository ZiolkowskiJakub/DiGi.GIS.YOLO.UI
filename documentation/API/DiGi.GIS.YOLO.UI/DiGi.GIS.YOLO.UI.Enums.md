#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Enums Namespace
### Enums

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource'></a>

## LegacySource Enum

Names which record says a building may have been seen by the `train8` detector, whose dataset is lost\.

Two independent records are consulted and a building is Legacy when either says so - the union is conservative, so doubt removes a building from the clean evaluation subset rather than adding one. Anything other than [None](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.None 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.None') is Legacy, and [Unknown](DiGi.GIS.YOLO.UI.Enums.md#DiGi.GIS.YOLO.UI.Enums.LegacySource.Unknown 'DiGi\.GIS\.YOLO\.UI\.Enums\.LegacySource\.Unknown') is Legacy too: a building whose history could not be read is never reported as clean by default.

```csharp
public enum LegacySource
```
### Fields

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource.Unknown'></a>

`Unknown` -1

The history could not be read, so the decision was not taken\. Treated as Legacy\.

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource.None'></a>

`None` 0

Neither record places the building in the `train8` dataset\.

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource.Tsv'></a>

`Tsv` 1

Only the legacy reference list \- the regressor training table built days after `train8` \- names the building\.

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource.Timestamp'></a>

`Timestamp` 2

Only the stored history names the building: it carries a user entry that is undated or dated before the cut\-off\.

<a name='DiGi.GIS.YOLO.UI.Enums.LegacySource.Both'></a>

`Both` 3

Both records name the building\.

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

The options could not be loaded, name no county or no scratch directory, or narrow the feature projection the model was trained on\. Nothing was attempted\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Environment'></a>

`Environment` 2

The preflight found that this machine cannot run the detector or score its buildings \- no CPython carrying ultralytics, no weights, or no year built model\. Nothing was exported\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Authorization'></a>

`Authorization` 3

The Web API authorization key is missing, or the client could not be built from it\. Nothing was read or written\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Failed'></a>

`Failed` 4

The run started and one or more of its steps did not complete\. What it managed to write is written\.

<a name='DiGi.GIS.YOLO.UI.Enums.YearBuiltPredictionExitCode.Cancelled'></a>

`Cancelled` 5

The run was stopped before it finished\. What it had already written is committed\.

<a name='DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset'></a>

## YOLODetectorEvaluationSubset Enum

Names the subset of the Test buildings a detector evaluation row covers\.

```csharp
public enum YOLODetectorEvaluationSubset
```
### Fields

<a name='DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset.All'></a>

`All` 0

Every Test building with at least one image\.

<a name='DiGi.GIS.YOLO.UI.Enums.YOLODetectorEvaluationSubset.Clean'></a>

`Clean` 1

The Test buildings the `train8` detector cannot have seen \- Test and not Legacy\. A building whose Legacy decision could not be taken is not in it\.