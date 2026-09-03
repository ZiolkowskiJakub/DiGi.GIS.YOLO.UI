# Implementation Plan — Issue #5

**Issue:** [The year built read is one request per building, and the bulk endpoint it says does not exist is live](https://github.com/ZiolkowskiJakub/DiGi.GIS.YOLO.UI/issues/5)
**Verdict:** **Still valid.** Every premise was verified against the source and the deployed host on 2026-09-03. One additional wire-contract finding (§1) is not in the issue body.
**Scope:** `DiGi.GIS.YOLO.UI` (client) + `DiGi.GIS.YOLO.UI.xUnit` (tests) only. The WebAPI side already ships everything needed; no server change.
**Tier:** `ai: standard` — the existing label is correct (one `Query` member, its call site, a constant, and its `[Fact]`s).

---

## 1. Verified premises

| # | Premise in the issue | Verdict | Evidence |
|---|---|---|---|
| 1 | The read issues one `GET gis/yearbuiltdata/itemsbyreference` per building, eight at a time | ✅ | [YearBuiltDatasAsync.cs](../DiGi.GIS.YOLO.UI/Query/YearBuiltDatasAsync.cs) — `CreateHttpClient<YearBuiltDataController>(nameof(YearBuiltDataController.GetItemsByReferenceAsync), …)` and the `for (int i = 0; i < references.Count; i += concurrencyLimit)` loop |
| 2 | `POST gis/yearbuiltdata/itemsbyreferences` is live on the deployed host | ✅ | `YearBuiltDataController.cs:429-435` in `DiGi.GIS.WebAPI`; present in the public live catalog `GET https://api.digiproject.uk/information/endpoints` (action `GetItemsByReferences`, `POST`); probe with a bogus reference → **204**, bogus route on the same controller → **404** (control) |
| 3 | The stale summary sentence is still there | ✅ | `YearBuiltDatasAsync.cs` summary, fourth paragraph: *"There is no bulk read for this table…"* |
| 4 | The feature read already pages against the endpoint cap | ✅ | [Constants/Count.cs](../DiGi.GIS.YOLO.UI/Constants/Count.cs) `BuildingDataReference_Maximum = 10000`; [RunYearBuiltPredictionsAsync.cs](../DiGi.GIS.YOLO.UI/Modify/RunYearBuiltPredictionsAsync.cs) clamps `ReferenceBatchSize` to it; `Query/BuildingDataTableAsync.cs` — *"the caller pages the references rather than this method doing it - a page is the unit that succeeds or fails"* |
| 5 | A failed read strands a **permanent** fresh row | ✅ | `yearBuiltData ??= new YearBuiltData(reference)` in the catch block; [DiGi.GIS.PostgreSQL#7](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/7) (open) confirms writers append without deleting — a rebuilt object (new GUID) stores a second row alongside the unread one |
| 6 | The test infrastructure for the proposed `[Fact]` exists | ✅ | `StubHttpClientFactory`/`StubHttpMessageHandler` in [Facts/ExportPredictionImages.cs](../../DiGi.Test/DiGi.GIS.YOLO.UI.xUnit/Facts/ExportPredictionImages.cs) (private in the partial `Facts` class), already used by [Facts/RunYearBuiltPredictionsFeatureCoverage.cs](../../DiGi.Test/DiGi.GIS.YOLO.UI.xUnit/Facts/RunYearBuiltPredictionsFeatureCoverage.cs) |

### Finding not in the issue — the fallback flag

The singular endpoint reads with `fallbackByReference = true`
(`YearBuiltDataController.cs:273` — *"perform a fallback search by reference alone for any references not found in the initial [county] search"*),
while the bulk endpoint binds `fallbackByReference` with a **`false` default** (`YearBuiltDataController.cs:435`).

The new client **must send `fallbackbyreference=true` explicitly**. An omitted parameter is not a binding failure (WebAPI Contracts §2) — it silently keeps the server default. Without the flag, a stored row filed under a sibling polygon part (or with a null county) is no longer read back, the code falls through to a fresh `YearBuiltData`, and the write stores exactly the permanent duplicate row this change exists to eliminate.

---

## 2. Wire contract (verified against source and the live host)

`POST https://api.digiproject.uk/gis/yearbuiltdata/itemsbyreferences`

| Wire element | Value |
|---|---|
| Body | JSON array of reference strings (`[FromBody] IEnumerable<string>`), 1…10 000 entries — more than `referenceCount_Maximum` (`YearBuiltDataController.cs:20`) → **400** |
| Query `countyid` | optional server-side, but required for correct partition addressing — the current client already sends it |
| Query `fallbackbyreference` | **send `true`** — preserves the singular endpoint's read semantics (§1) |
| Query `commandtimeout` | server default 30 s — do not send |
| 200 | `List<YearBuiltData>` (GIS wire form: PascalCase, `_type` discriminator) when at least one stored row matched |
| 204 | no stored row matched — a building with nothing stored is not an error; `PostAsync<T>` reports this as `Succeeded = true`, `Result = null` |
| 500 | database read failed — `PostAsync<T>` throws |
| Transient 502/503/504/408/429 | `PostAsync<T>` already retries per `PostOptions.RetryCount` (default 3, 2 s backoff doubling) — the built-in "retry the page" for flaky failures |

Other relevant facts:

- A reference can carry several stored rows (one per stored object); the current code takes the first (`[0]`) — **keep first-wins**.
- `PostAsync<T>(HttpClient, string, HttpContent, PostOptions)` and `GIS.WebAPI.Create.HttpContent(string, CancellationToken)` are the same plumbing `Query/BuildingDataTableAsync.cs` uses — reuse them, do not inline new HTTP.
- `UrlBuilder.AddParameter(name, bool)` exists and renders `True`; ASP.NET bool binding is case-insensitive.
- `YearBuiltData.Reference` is the public read-back property for mapping response items to references; `YearBuiltData(string?)` is the cheap fresh-object constructor.
- `SetPredictedYearBuilt` only fails when `IYearBuilt.Source` is null, which `PredictedYearBuilt.Source` (computed, never null) cannot produce — the existing `if (…)` guard is a safety net, keep it.

---

## 3. Changes

### 3.1 `DiGi.GIS.YOLO.UI/Constants/Count.cs`

Add, beside `BuildingDataReference_Maximum`:

```csharp
public const int YearBuiltDataReference_Maximum = 10000;
```

Mirrors the cap `YearBuiltDataController` enforces. One constant per endpoint, named after it, so the two caps can diverge independently — the existing `BuildingDataReference_Maximum` pattern.

### 3.2 `DiGi.GIS.YOLO.UI/Query/YearBuiltDatasAsync.cs` — the core change

**Signature.** Replace `int maxConcurrentRequests = 8` with `int referenceBatchSize = Constants.Count.YearBuiltDataReference_Maximum`, clamped in the body with the existing idiom `referenceBatchSize < 1 ? 1 : Math.Min(referenceBatchSize, Constants.Count.YearBuiltDataReference_Maximum)`. Still eight parameters — keep one per line (DIGI0001), `CancellationToken` last.

**`readStored == false` path: unchanged.** No HTTP, fresh datum per reference — the column-only write case.

**`readStored == true` path.** Resolve `CreateHttpClient<YearBuiltDataController>(nameof(YearBuiltDataController.GetItemsByReferencesAsync), out path)` (unresolvable → log + empty result, as today). Replace the `concurrencyLimit`/`Task.Run` fan-out with a sequential page loop, shaped like the feature read (`RunYearBuiltPredictionsAsync.cs:337-344`):

1. `List<string> references_Page = references.GetRange(i, Math.Min(referenceBatchSize, references.Count - i));`
2. Body: `JsonArray` of the page's references → `await GIS.WebAPI.Create.HttpContent(json, cancellationToken)`. The body is the raw array — the parameter type is `IEnumerable<string>`, so no parameter-object member mapping is needed (unlike `BuildingDataTableAsync`).
3. URI: `new UrlBuilder(path).AddParameter("countyid", countyId).AddParameter("fallbackbyreference", true).ToString()` — `fallbackbyreference` is load-bearing (§1).
4. `PostResponse<List<YearBuiltData>?> postResponse = await DiGi.WebAPI.Modify.PostAsync<List<YearBuiltData>>(httpClient, requestUri, httpContent, postOptions_Temp);` — 204 arrives as `Succeeded = true`, `Result = null` → empty map → the whole page is fresh data (correct first-write case).
5. Merge — **unchanged semantics**: build `Dictionary<string, YearBuiltData>` from the page response keyed by `.Reference` (first wins, matching the current `[0]`); then per reference **in page order**: `stored ?? new YearBuiltData(reference)` → `SetPredictedYearBuilt(dateTime, year)` → append.
6. **Page failure** (non-transient throw or `!Succeeded`): log with the page size and county (the `BuildingDataTableAsync.cs:92` idiom) and **skip the page — no fresh datum is fabricated for it**. Transient failures are already retried by `PostOptions`.

**Documentation in the same change** (the issue's point):

- Replace the stale *"There is no bulk read for this table…"* paragraph: the read is bulk, paged to `Constants.Count.YearBuiltDataReference_Maximum`, a page is the unit that succeeds or fails, and a failed page is skipped rather than stranding a fresh stored object.
- Update the `readStored` `<param>`: *"…this is a request each"* is stale too — it is now one request per page of up to ten thousand.
- Keep `<param>` order mirroring the signature (General §1.8).

**Remove:** the `ConcurrentBag`, the `Task.Run` fan-out, the per-reference catch block and its *"a second stored object for a building whose existing one could not be read"* comment — the concern it named is now handled at page granularity, and the comment's worst case no longer exists.

### 3.3 `DiGi.GIS.YOLO.UI/Modify/RunYearBuiltPredictionsAsync.cs` — the call site

At the `YearBuiltDatasAsync` call (line 452):

- Pass the already-clamped `referenceBatchSize` (lines 66-67) instead of `maxConcurrentRequests`.
- Pass `postOptions_Bulk` instead of `postOptions_Item` — a bulk read is sized against the sixty-second budget, like the feature read.
- After the call, add the page-failure signal:

```csharp
if (yearBuiltDatas.Count < years_ByReference.Count)
{
    Fail(nameof(Query.YearBuiltDatasAsync), countyId);
}
```

A shortfall is unambiguous: every reference that is read yields exactly one datum, so a smaller result means at least one page was skipped. Same idiom as `Fail(nameof(Query.BuildingDataTableAsync), countyId)` two hundred lines above.

`maxConcurrentRequests` and the `MaxConcurrentRequests` option **stay** — the image-export leg (`ExportPredictionImages`) still uses them.

**Intended semantics change** (the issue's "worth fixing alongside"): a failed page no longer strands a fresh object. Those buildings carry no prediction this run, the step is flagged in `YearBuiltPredictionResult.FailedStepNames`, and a re-run merges them correctly — still no duplicate. Partial counties are already tolerated by the pipeline (a failed write batch `Fail`s and continues).

### 3.4 `DiGi.Test/DiGi.GIS.YOLO.UI.xUnit/Facts/YearBuiltPredictionRoutes.cs`

The fact exists to assert *"the routes the pipeline resolves from the controller types are the ones the deployed host actually serves."* After this change the pipeline resolves the bulk route, so swap the singular assertion for:

```csharp
string? path_YearBuiltData = DiGi.WebAPI.Query.Path<YearBuiltDataController>(nameof(YearBuiltDataController.GetItemsByReferencesAsync));
Assert.Equal("gis/yearbuiltdata/itemsbyreferences", path_YearBuiltData, ignoreCase: true);
```

(the bulk route is in the live public endpoint catalog — verified 2026-09-03; refresh the fact's date line).

### 3.5 New `Facts/YearBuiltDatas.cs`

Per §4.

### 3.6 Documentation

`documentation/API/` is DefaultDocumentation-generated — it regenerates from the XML docs at build. No hand edits.

---

## 4. Tests (per *Coding - Automatic Tests*)

**Order: write the facts first and watch them fail on the unmodified code** (Reproduce Before Fixing — the reported symptom is *one request per reference*; a fact asserting paged bulk POSTs sees zero of them today).

`Facts/YearBuiltDatas.cs`, `public partial class Facts`, `async Task` facts with XML summaries, reusing the existing `StubHttpClientFactory` from `Facts/ExportPredictionImages.cs`:

1. **`YearBuiltDatas_PagedBulkRead`** — 25 references, `referenceBatchSize: 10`, stub answers 204:
   - exactly **3** requests to `gis/yearbuiltdata/itemsbyreferences`, and **zero** to `gis/yearbuiltdata/itemsbyreference`;
   - page sizes 10/10/5, references in order, each URI carrying `countyid` **and** `fallbackbyreference=True`;
   - result: 25 fresh data, each carrying this run's prediction under the run stamp.
2. **`YearBuiltDatas_MergesStoredEntries`** — stub answers page 1 with a 200 JSON body holding a stored entry (with one existing history entry) for a single reference, page 2 with 204:
   - the stored object is returned with its GUID intact, its prior history preserved, and this run's prediction added;
   - every other reference is a fresh datum.
3. **`YearBuiltDatas_FailedPageSkipped`** — the "worth fixing alongside" behavior: stub answers page 1 with 200, page 2 with **500** (`PostAsync` throws on non-transient):
   - the result contains exactly page 1's data and **none** of page 2's references — no fresh datum fabricated for an unread page (this assertion fails on today's code, which fabricates one per failed read).
4. **Orchestrator level** — `UpdateYearBuiltData = true`, following the `RunYearBuiltPredictions_FeatureCoverage_*` pattern:
   - year-built page 500s → `Assert.Contains(nameof(Query.YearBuiltDatasAsync), result.FailedStepNames)`;
   - 204 variant → the step is **not** flagged and `YearBuiltDataUpdatedCount` equals the expected count.

---

## 5. Verification (per the issue)

1. **Build + suite:** Release build with zero warnings (General §1.4), then the full `DiGi.GIS.YOLO.UI.xUnit` suite; re-run the new facts **isolated** (`--filter "FullyQualifiedName~YearBuiltDatas"`) for clean figures.
2. **Read-only live measurement (safe to run now):** take one page of up to 10 000 real references for a county and time, on `api.digiproject.uk`, (a) the current pattern — N singular GETs at cap 8 — against (b) one bulk POST. Record the ratio on the issue. No write endpoint is touched.
3. **Full write-run timing (production write — explicit user approval required first):** time the year-built write leg over county `104106` with the pre-change build and the post-change build, as the issue asks; record both figures in a comment. Per *Coding - Deployed WebAPI* §3, POSTs to write endpoints on the production host need explicit authorization.
4. **Close the issue** with the standard resolution comment (resolution & commits, summary of changes, automated tests, live verification figures) once the user signs off.

---

## 6. Guideline compliance

| Guideline | How the plan honours it |
|---|---|
| *Coding - General* §1.6, §1.8 | eight parameters → one per line; `CancellationToken` last; no `var`; block-scoped namespace; collection expressions; named `cancellationToken:` at call sites |
| *Coding - General* §2 | one member per file — the method keeps its name and its file; no new `Query` member |
| *Coding - WebAPI Contracts* §1, §2 | both sides of the wire diffed by hand (§2); `fallbackbyreference` sent explicitly because an omitted parameter keeps the server default and would read as "no filter"; parameter names checked against the deployed catalog |
| *Coding - WebAPI Contracts* §4 | the endpoint **is** deployed (verified) — no `TODO [Marker]` gating needed |
| *Coding - Deployed WebAPI* §3 | only read-only probes so far; the write-run timing is gated on explicit approval; no tests added to `DiGi.Test` for live-endpoint behaviour |
| *Coding - Automatic Tests* §4 | Reproduce Before Fixing; isolated measurement; the existing stub pattern is reused, not reinvented |
| *GitHub - Issues* §2 | every premise checked against code and the live host — all held; the `fallbackbyreference` finding is an addition and should be posted to the issue alongside the plan |
| *GitHub - AI Issue Classification* §2.B | `ai: standard` — one `Query` member and its facts; label already correct, no re-tier |

---

## 7. Risks & notes

- **Public API change:** the `maxConcurrentRequests` → `referenceBatchSize` parameter swap on a public static extension. Verified there is exactly one caller (`RunYearBuiltPredictionsAsync`) and none in the xUnit project, and this is a client library, not a deployed WebAPI — no wire contract is involved.
- **Already-stranded rows** from pre-fix write runs (county `104106` among them) are not repaired by this change: the bulk read returns both rows and first-wins keeps today's behaviour. Retiring them belongs to [DiGi.GIS.PostgreSQL#7](https://github.com/ZiolkowskiJakub/DiGi.GIS.PostgreSQL/issues/7) (the missing delete half of the writers).
- **Partial county on page failure:** the other pages still write; the step is flagged and the run is reported as not fully successful. A re-run is safe — merging under the run stamp is idempotent.
- **`PostOptions` retry already covers transient failures**; the explicit skip only triggers on a hard 400/500, which is the rare case the old code got wrong.
