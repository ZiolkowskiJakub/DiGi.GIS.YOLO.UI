# Implementation Plan — Issue #6

> **Naming note (supersedes §3.2–§3.5):** the interface member was implemented as `YearBuiltPredictorReadiness()`, the full type name, per the owner's explicit approval — not the short `Readiness()` this plan shows.

**Issue:** [No preflight for the ML.NET model: a runner without it exports a whole county before failing](https://github.com/ZiolkowskiJakub/DiGi.GIS.YOLO.UI/issues/6)
**Verdict:** **Still valid.** Every premise verified against the source on 2026-09-04. One design question the issue leaves open (§2) is resolved by *Coding - General* §1.13.
**Scope:** `DiGi.GIS.IO` (seam type + one interface member), `DiGi.GIS.ML` (predictor + model readiness partial), `DiGi.GIS.YOLO.UI` (orchestrator preflight + exit code), `DiGi.GIS.YOLO.UI.xUnit` (stub + one new `[Fact]`). No WebAPI, no database, no NuGet, no wire contract.
**Tier:** `ai: standard` (current label) is defensible for the readiness-only scope; the change does modify a core abstraction across three repos, which *GitHub - AI Issue Classification* §3 reads as `ai: heavy` — see §6.

---

## 1. Verified premises

| # | Premise in the issue | Verdict | Evidence |
|---|---|---|---|
| 1 | The Python side is prefetched before a single image is exported | ✅ | [RunYearBuiltPredictionsAsync.cs](../DiGi.GIS.YOLO.UI/Modify/RunYearBuiltPredictionsAsync.cs) — the `if (…RunPrediction)` block calls `DiGi.YOLO.Query.YOLOEnvironmentResult(…)` and `return Result()` on `!Runnable`, before the per-county loop |
| 2 | Nothing does the same for the ML.NET model | ✅ | [IYearBuiltPredictor.cs](../../DiGi.GIS.IO/DiGi.GIS.IO/Interfaces/IYearBuiltPredictor.cs) declares `Table? Predict(Table?)` and nothing else; the `Score` leg has no preflight |
| 3 | `MLNetModelPath` probes candidates and falls through to `Path.GetFullPath(fileName)` | ✅ | [OrtoBuildingDetectionModel.consumption.cs](../../DiGi.GIS.ML/DiGi.GIS.ML/OrtoBuildingDetectionModel.consumption.cs) (auto-generated file) — `MLNetModelPath` ends in `return Path.GetFullPath(fileName);` |
| 4 | The engine is a `Lazy` in `ExecutionAndPublication` mode, so the first `FileNotFoundException` is cached and rethrown for the life of the process | ✅ | same file — `public static readonly Lazy<…> PredictEngine = new(CreatePredictEngine, true);` (`true` = `LazyThreadSafetyMode.ExecutionAndPublication`); `CreatePredictEngine` calls `mlContext.Model.Load(MLNetModelPath, …)` |
| 5 | The model reaches the runner by a single `<None Include>` that a deployment can omit | ✅ | [DiGi.GIS.YOLO.UI.ConsoleApp.csproj](../DiGi.GIS.YOLO.UI.ConsoleApp/DiGi.GIS.YOLO.UI.ConsoleApp.csproj) — `<None Include="..\..\DiGi.GIS.ML\DiGi.GIS.ML\OrtoBuildingDetectionModel.mlnet" … CopyToOutputDirectory="PreserveNewest" />` |
| 6 | The seam exists precisely to keep `DiGi.GIS.ML` out of the orchestrator's dependency set | ✅ | [IYearBuiltPredictor.cs](../../DiGi.GIS.IO/DiGi.GIS.IO/Interfaces/IYearBuiltPredictor.cs) summary — "A direct reference drags Microsoft.ML, … TorchSharp-cpu and Plotly.NET into every host that loads the orchestrator." |

### Finding the issue does not state — the readiness probe must not live in the generated file

`OrtoBuildingDetectionModel.consumption.cs` carries an auto-generated header and is regenerated on every retrain. *Coding - General* §1.13 records a past incident where one regeneration reverted five hand-fixes at once, **including the very `MLNetModelPath` resolver this fix depends on**. So the new readiness surface must be a **sibling partial** the Model Builder does not own, not a member added to `consumption.cs`. This is the resolution of the design question the issue leaves open.

### Finding the issue does not state — the existing facts pin the null-predictor behaviour

[Facts/RunYearBuiltPredictions.cs](../../DiGi.Test/DiGi.GIS.YOLO.UI.xUnit/Facts/RunYearBuiltPredictions.cs) already asserts, for `Score = true` and a **null** predictor, that the run proceeds to the per-county loop, `BuildingCount` is `2`, and `FailedStepNames` contains `nameof(IYearBuiltPredictor)` (`RunYearBuiltPredictions_MissingPredictor` — "The seam is optional by design"). So the null-predictor case must **stay** where it is (a stated per-county step failure). The new preflight must fire only when a predictor is actually supplied *and* it reports itself unrunnable — otherwise that fact breaks.

---

## 2. The seam contract (design)

The issue offers "a `bool Runnable` or a small readiness answer" and notes the two issues are "worth designing together rather than adding two unrelated members to the interface" ([DiGi.GIS.ML#6](https://github.com/ZiolkowskiJakub/DiGi.GIS.ML/issues/6) for the year range and radiuses). This plan honours both:

- **One interface member, not two.** `IYearBuiltPredictor` gains a single `Readiness()` that returns a small result type. A bare `bool` would force ML#6 to add a second, unrelated member — the exact thing the issue warns against.
- **The type carries the answer *and* the diagnostics.** `bool Runnable` alone cannot say *why* it cannot score; the result type carries `Messages`, mirroring `DiGi.YOLO.Classes.YOLOEnvironmentResult` (`Runnable` + `Messages`), which the orchestrator already folds into its result.
- **The type is the extension point for ML#6.** It is designed so ML#6 adds `Years`/`Radiuses` (the contract the loaded model was trained on) to the *same* type and the *same* orchestrator check — no second interface member. This issue implements only the runnability half.
- **The type lives in `DiGi.GIS.IO`, not `DiGi.GIS.ML`.** The orchestrator must name the return type to read it; if the type were in `DiGi.GIS.ML`, the orchestrator would have to reference `DiGi.GIS.ML`, undoing the seam. `DiGi.GIS.IO` is the only assembly both sides already reference, and it is where the interface lives.
- **Not serializable.** `YOLOEnvironmentResult` is a `SerializableResult` because it crosses the WebAPI. `YearBuiltPredictorReadiness` is computed in the host and consumed in the same call, so it is a plain `sealed` data class — no `SerializableObject`, which also means no `SerializationCheck` fact is required (*Coding - Automatic Tests* §4).

The one dependency on the generated file is recorded, per *Coding - General* §1.13, in `OrtoBuildingDetectionModel.provenance.md` so a retrain does not silently lose it.

---

## 3. Changes

### 3.1 `DiGi.GIS.IO/Classes/YearBuiltPredictorReadiness.cs` — new type

A new `Classes/` folder (matching `DiGi.YOLO.Classes`, `DiGi.GIS.ML.Classes`, `DiGi.GIS.YOLO.UI.Classes`); `DiGi.GIS.IO` currently has only `Constants/`, `Create/`, `Enums/`, `Interfaces/`, `Modify/`, `Query/`.

```csharp
using System.Collections.Generic;

namespace DiGi.GIS.IO.Classes
{
    /// <summary>
    /// States whether a <see cref="Interfaces.IYearBuiltPredictor"/> can score at all, probed before a run starts.
    /// <para>The seam returns this rather than a bare flag so the reason a predictor cannot score travels with the answer - an unattended run learns in seconds that the trained model is missing rather than after exporting a county of imagery. It is the single surface the orchestrator checks, so the contract a predictor expects (the year range and radiuses the loaded model was trained on, ZiolkowskiJakub/DiGi.GIS.ML#6) lands beside this runnability instead of as a second, unrelated member on the interface.</para>
    /// <para>It is a local probe result, computed in the host and consumed in the same call, so it is not a SerializableObject and carries no serialization surface.</para>
    /// </summary>
    public sealed class YearBuiltPredictorReadiness
    {
        /// <summary>Gets whether the predictor can score at all.</summary>
        public bool Runnable { get; }

        /// <summary>Gets the diagnostics that explain the answer - why the predictor cannot score. Empty when it can score.</summary>
        public List<string> Messages { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="YearBuiltPredictorReadiness"/> class.
        /// </summary>
        /// <param name="runnable">Whether the predictor can score at all.</param>
        /// <param name="messages">The diagnostics explaining why it cannot score. Null or empty when it can score.</param>
        public YearBuiltPredictorReadiness(bool runnable, IEnumerable<string>? messages = null)
        {
            this.Runnable = runnable;
            this.Messages = messages is null ? [] : [.. messages];
        }
    }
}
```

`List<string>` (not `IReadOnlyList`) and the collection expression match the existing `DiGi.GIS.IO` convention ([Query/YearBuiltPredictionInputColumns.cs](../../DiGi.GIS.IO/DiGi.GIS.IO/Query/YearBuiltPredictionInputColumns.cs) — `List<Column> result = [];`) and build under the assembly's `netstandard2.0` target.

### 3.2 `DiGi.GIS.IO/Interfaces/IYearBuiltPredictor.cs` — the seam gains one member

```csharp
/// <summary>
/// Reports whether this predictor can score at all, probed before a run starts.
/// <para>The orchestrator checks this beside the Python preflight when the scoring step is on, so a runner that is missing the model it scores with is refused in seconds instead of after exporting a county of imagery and failing on the first scoring batch. It carries the diagnostics that say why, rather than a bare flag.</para>
/// </summary>
/// <returns>The readiness of this predictor.</returns>
DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness Readiness();
```

Public API break: both implementers (§3.4, §4) are updated in the same change; no other implementer exists (verified across the workspace).

### 3.3 `DiGi.GIS.ML/DiGi.GIS.ML/OrtoBuildingDetectionModel.readiness.cs` — new sibling partial

A partial the Model Builder does not generate, so a retrain does not revert it (*Coding - General* §1.13). It is a part of the same type in the same assembly, so it may read the private `MLNetModelPath` that the generated file resolves — one resolver, two uses, no duplicated discovery logic.

```csharp
using System.IO;

namespace DiGi_GIS_ML
{
    /// <summary>
    /// Readiness surface for the generated model, kept in a partial the Model Builder does not own so a retrain does not revert it.
    /// <para>A hand-fix inside OrtoBuildingDetectionModel.consumption.cs would be regenerated away (General §1.13); a sibling partial is where the correction survives. Its one dependency - the hand-maintained private MLNetModelPath - is recorded in OrtoBuildingDetectionModel.provenance.md so a retrain re-establishes it.</para>
    /// </summary>
    public partial class OrtoBuildingDetectionModel
    {
        /// <summary>Gets whether the trained model file is present at the resolved path.</summary>
        public static bool IsModelAvailable => File.Exists(MLNetModelPath);

        /// <summary>Gets the resolved model path, for the diagnostic when it is not present.</summary>
        public static string ResolvedModelPath => MLNetModelPath;
    }
}
```

### 3.4 `DiGi.GIS.ML/DiGi.GIS.ML/Classes/YearBuiltPredictor.cs` — the implementation

```csharp
/// <summary>
/// Reports whether this predictor can score at all.
/// <para>Answers from the generated model's readiness surface: the trained file must be present at its resolved path, or the first scoring batch throws and the Lazy caches the failure for the life of the process.</para>
/// </summary>
/// <returns>Runnable when the model file is present; otherwise not runnable, carrying the path it looked for.</returns>
public DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness Readiness()
{
    if (OrtoBuildingDetectionModel.IsModelAvailable)
    {
        return new DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness(true);
    }

    return new DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness(
        false,
        [string.Format(System.Globalization.CultureInfo.InvariantCulture, "The year built model was not found at {0}. The trained model file must be present beside the runner.", OrtoBuildingDetectionModel.ResolvedModelPath)]);
}
```

(`using DiGi_GIS_ML;` added, as [Query/PredictedYearBuilts.cs](../../DiGi.GIS.ML/DiGi.GIS.ML/Query/PredictedYearBuilts.cs) already does.)

### 3.5 `DiGi.GIS.YOLO.UI/DiGi.GIS.YOLO.UI/Modify/RunYearBuiltPredictionsAsync.cs` — the preflight

Inserted **immediately after** the `if (…RunPrediction)` Python block and before the `CountyReferencesAsync` scope read — i.e. beside the Python preflight, before any WebAPI call or per-county work. Gated on `Score` **and** a non-null predictor, so the null-predictor case stays per-county (§1, finding 2):

```csharp
// The model file is as much a prerequisite as the interpreter: a runner without it exports a
// county of imagery and fails on the first scoring batch, and the Lazy caches that
// FileNotFoundException for the life of the process, so every county behind it fails the same way.
if (yearBuiltPredictionPipelineOptions.Score && yearBuiltPredictor is not null)
{
    DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness yearBuiltPredictorReadiness = yearBuiltPredictor.Readiness();
    if (!yearBuiltPredictorReadiness.Runnable)
    {
        messages.AddRange(yearBuiltPredictorReadiness.Messages);
        failedStepNames.Add(nameof(DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness));

        Serilog.Modify.Log(Serilog.Enums.LogEventLevel.Error, "{Method}: this machine cannot score the buildings - {Messages}", nameof(RunYearBuiltPredictionsAsync), string.Join("; ", yearBuiltPredictorReadiness.Messages));

        return Result();
    }
}
```

The per-county `if (yearBuiltPredictor is null) { Fail(nameof(IYearBuiltPredictor), countyId); continue; }` stays untouched.

### 3.6 `DiGi.GIS.YOLO.UI.ConsoleApp/Program.cs` — the exit code

The readiness refusal is a preflight, the same kind of failure as the Python one — "a machine that cannot start the detector at all is a different thing to fix than a step that failed while running" ([YearBuiltPredictionExitCode.cs](../DiGi.GIS.YOLO.UI/Enums/YearBuiltPredictionExitCode.cs) — the enum's own words) — so it maps to `Environment`, not `Failed`:

```csharp
bool preflightFailed = failedStepNames.Contains(nameof(DiGi.YOLO.Query.YOLOEnvironmentResult))
    || failedStepNames.Contains(nameof(DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness));
return (int)(preflightFailed ? YearBuiltPredictionExitCode.Environment : YearBuiltPredictionExitCode.Failed);
```

### 3.7 `DiGi.GIS.ML/DiGi.GIS.ML/OrtoBuildingDetectionModel.provenance.md` — the §1.13 record

Add a line: the hand-maintained `OrtoBuildingDetectionModel.readiness.cs` partial depends on the generated file's private `MLNetModelPath`; a retrain must keep both. This is the "record every fix where regeneration cannot reach it" the guideline requires.

### 3.8 Documentation

`documentation/API/` is DefaultDocumentation-generated and regenerates from the XML docs at build — no hand edits. Rebuild `DiGi.GIS.IO` so its `DiGi.GIS.IO.xml` (and the interface doc) picks up the new member and type.

---

## 4. Tests (per *Coding - Automatic Tests*)

**Order: extend the stub, add the fact, and watch the fact fail on the unmodified orchestrator** (Reproduce Before Fixing — a fact asserting the run refuses when the predictor reports unrunnable passes only once §3.5 exists).

`Facts/RunYearBuiltPredictions.YearBuiltPredictorStub.cs` — the stub implements the new member. It **defaults to runnable** so the two existing facts keep passing, and takes an optional flag for the new one:

```csharp
private readonly bool runnable;

public YearBuiltPredictorStub(short year, bool runnable = true)
{
    this.year = year;
    this.runnable = runnable;
}

public DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness Readiness()
{
    if (this.runnable)
    {
        return new DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness(true);
    }

    return new DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness(false, ["the year built model is missing (stub)"]);
}
```

`Facts/RunYearBuiltPredictions.cs` — a new fact, shaped like `RunYearBuiltPredictions_StubPredictor` but asserting the refusal happens **before** the per-county loop:

```csharp
[Fact]
public async Task RunYearBuiltPredictions_UnrunnablePredictor()
{
    // …scratch setup identical to the sibling facts…
    GISWebAPIManager gisWebAPIManager = new(null);
    YearBuiltPredictorStub yearBuiltPredictorStub = new(1965, runnable: false);

    Classes.YearBuiltPredictionPipelineOptions yearBuiltPredictionPipelineOptions = new()
    {
        CountyIds = [countyId],
        ScratchDirectory = directory_Scratch,
        ExportImages = false,   // even if it slipped past the preflight, it would not export
        RunPrediction = false,
        Score = true,
        UpdateDetections = false,
        UpdatePredictedYearBuilt = false,
        UpdateYearBuiltData = false
    };

    Classes.YearBuiltPredictionResult? yearBuiltPredictionResult = await gisWebAPIManager.RunYearBuiltPredictionsAsync(yearBuiltPredictorStub, yearBuiltPredictionPipelineOptions);

    Assert.NotNull(yearBuiltPredictionResult);
    Assert.Equal(0, yearBuiltPredictorStub.CallCount);   // never asked to score
    Assert.Equal(0, yearBuiltPredictionResult!.BuildingCount); // no county was carried through
    Assert.Contains(nameof(DiGi.GIS.IO.Classes.YearBuiltPredictorReadiness), yearBuiltPredictionResult.FailedStepNames);
    Assert.Contains("model", string.Join(" ", yearBuiltPredictionResult.Messages)); // the diagnostic travels
}
```

The three existing facts keep their assertions and stay green: `_MissingPredictor` (null predictor, `Score = true`) still reaches the per-county loop because the preflight is gated on a non-null predictor; `_StubPredictor` (runnable stub) still passes the preflight and stops at the feature read; `_Validation` (`Score = false`) never reaches the preflight.

---

## 5. Verification (per the issue)

1. **Build order matters (HintPath references):** `DiGi.GIS.IO` and `DiGi.GIS.ML` are consumed as pre-built DLLs, so rebuild them before `DiGi.GIS.YOLO.UI`. Release build with zero warnings (*Coding - General* §1.4), then the full `DiGi.GIS.YOLO.UI.xUnit` suite; re-run the new fact isolated (`--filter "FullyQualifiedName~RunYearBuiltPredictions_UnrunnablePredictor"`).
2. **The issue's manual check:** point a runner at a directory with no `.mlnet` and run with `Score = true`. Expected: it exits within seconds with `YearBuiltPredictionExitCode.Environment` (2) and the "model was not found at …" diagnostic, instead of exporting the county and failing on the first scoring batch.
3. **Close the issue** with the standard resolution comment (resolution & commits, summary of changes, automated tests, the exit-code/seconds figure) once the user signs off.

---

## 6. Guideline compliance

| Guideline | How the plan honours it |
|---|---|
| *Coding - General* §1.13 | the readiness surface is a **sibling partial**, not a member of the generated `consumption.cs`; its dependency on the hand-maintained `MLNetModelPath` is recorded in `provenance.md` — the exact incident the guideline documents (a regeneration reverted the `MLNetModelPath` resolver) |
| *Coding - General* §1.2, §1.5 | explicit typing, block-scoped namespaces, collection expressions, target-typed `new()` — as written above |
| *Coding - General* §1.4 | zero-warnings build; the readiness type carries the diagnostics so nothing is swallowed |
| *Coding - General* (seam) | the new type is in `DiGi.GIS.IO`, so the orchestrator still does not reference `DiGi.GIS.ML` — the seam is preserved, not widened |
| *Coding - Editor Config* | `csharp_style_var_* = false`, `prefer_collection_expression = true`, block-scoped namespaces — the snippets comply; `DiGi.GIS.IO`'s `netstandard2.0` target is why `Messages` is a concrete `List<string>` |
| *XML Documentation - Create* | the new type, the interface member, the predictor method, and the partial members all carry `<summary>`; `<param>` order mirrors the signature |
| *Coding - Automatic Tests* §4 | Reproduce Before Fixing (the fact fails pre-§3.5); the existing stub pattern is extended, not reinvented; no `SerializationCheck` is required because the type is deliberately not a `SerializableObject` |
| *GitHub - AI Issue Classification* §3 | the change modifies a core abstraction (the seam) across three repos — "err on the higher tier if the task involves modifying core abstractions." `ai: standard` (current) is defensible for the readiness-only scope; `ai: heavy` is the more defensible tier. Recommend confirming before implementation |

---

## 7. Risks & notes

- **Public API break:** `IYearBuiltPredictor` gains a member. Verified implementers: `DiGi.GIS.ML`'s `YearBuiltPredictor` and the xUnit `YearBuiltPredictorStub` — both updated in the same change. No other implementer in the workspace. `DiGi.GIS.IO` is a client-side seam, not a deployed WebAPI, so there is no wire contract to break.
- **The null-predictor case is deliberately unchanged.** The existing `RunYearBuiltPredictions_MissingPredictor` fact pins the per-county refusal for a null predictor ("The seam is optional by design"). The new preflight is gated on a non-null predictor, so that fact stays green and the documented intent is preserved.
- **ML#6 is a separate change.** This plan introduces the shared type and the single `Readiness()` member so ML#6 can add `Years`/`Radiuses` and the narrower/wider comparison to the *same* surface — but it does not implement the contract half. That stays in [DiGi.GIS.ML#6](https://github.com/ZiolkowskiJakub/DiGi.GIS.ML/issues/6) (labeled `ai: heavy`).
- **The generated-file dependency is the one fragile edge.** If a retrain reverts `MLNetModelPath`, the readiness partial fails to build — loudly, not silently. The `provenance.md` record (§3.7) is what keeps that from happening in silence.
- **Exit-code semantics change (for the better).** A runner without the model now exits `Environment` (2) rather than `Failed` (4). Callers comparing against the named enum still compile; callers that treated "any non-zero" as failure are unaffected.
