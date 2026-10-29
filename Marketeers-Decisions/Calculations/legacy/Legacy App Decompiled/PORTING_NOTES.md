# MarketSimulator.Model.dll — Decompilation & Porting Notes

## Binary DLLs (`decompiled/lib/`)

Copied from the desktop app repo:

`D:\All Simulator\simulatorcode\simulatorC\MarketSimulator\lib` → `decompiled/lib/`

| DLL | Size | Decompiled? | Notes |
|-----|------|-------------|-------|
| `MarketSimulator.Model.dll` | 640 KB | **Yes** | Core simulation math |
| `MarketSimulator.Reports.dll` | 554 KB | **Yes** | Chart Analysis UI + Excel/PPT export |
| `UndoRedoFramework.dll` | 22 KB | No | Undo/redo |
| `CheckBoxComboBox.dll`, `CustomControl.OrientedTextControls.dll`, `Divelements.SandGrid.dll` | — | No | WinForms UI controls |
| `Microsoft.Office.Interop.*`, `Office.dll` | — | No | Excel/PPT interop |
| `System.Windows.Forms.DataVisualization.dll` | — | No | Charts |

`MarketSimulator.Model.dll` SHA-256: `FD724C19D00F949D15933AB26DCACE7E2531EFE01E657E22D075B73DEF013EF3`  
`MarketSimulator.Reports.dll` SHA-256: `14950671431B4669A20BABC9D60328433D03F787B3CB077948A01640FD4E7A4E`

---

## Decompilation — Model (2026-06-22)

| Item | Value |
|------|-------|
| Source DLL | `decompiled/lib/MarketSimulator.Model.dll` (640 KB) |
| Original path | `D:\All Simulator\simulatorcode\simulatorC\MarketSimulator\lib\MarketSimulator.Model.dll` |
| Tool | [ilspycmd](https://www.nuget.org/packages/ilspycmd) **9.1.0.7988** |
| .NET SDK | **8.0.422** (installed via winget; required for `dotnet tool`) |
| Output | `decompiled/MarketSimulator.Model/` (project layout, `-p`) |

**Command used:**

```powershell
dotnet tool install -g ilspycmd --version 9.1.0.7988
ilspycmd "decompiled/lib/MarketSimulator.Model.dll" -o decompiled/MarketSimulator.Model -p
```

**Why the first attempt failed:** Only the .NET *runtime* was installed (`C:\Program Files (x86)\dotnet`), not the SDK. Without the SDK, `dotnet tool install ilspycmd` cannot run. Latest ilspycmd (10.x) also failed on this machine; version **9.1.0.7988** installed successfully.

**Prior workaround (before this decompile):** API surface from pythonnet reflection (`decompiled_reference/ProjectCore_API.txt`) plus WinForms `StudyStructure.cs` — no C# source from the DLL.

---

## Decompilation — Reports (2026-07-13)

| Item | Value |
|------|-------|
| Source DLL | `decompiled/lib/MarketSimulator.Reports.dll` (554 KB) |
| Tool | ilspycmd **9.1.0.7988** |
| Output | `decompiled/MarketSimulator.Reports/` (project layout, `-p`) |

**Command used:**

```powershell
ilspycmd "decompiled/lib/MarketSimulator.Reports.dll" -o decompiled/MarketSimulator.Reports -p
```

### Reports source layout

| File | Role |
|------|------|
| `MarketSimulator.Reports/ChartForm.cs` | Chart Analysis tabs (~5.7k lines): Elasticity, Price Index, Shares & Revenues, Brand Interaction, SoM vs SoP, Fair Share, Validation, Introduction, Removal |
| `MarketSimulator.Reports/ExcelSheet.cs` | Excel / PowerPoint / Graph interop export (~500 lines) |
| `MarketSimulator.Reports/IncrementalConfiguration.cs` | Intro/Removal product lineup config dialog |
| `MarketSimulator.Reports/SKUAttributeConfiguration.cs` | Per-SKU attribute sweep config dialog |
| `MarketSimulator.Reports/StudyScenariosForm.cs` | Scenario picker used by chart tabs |

**Porting note:** Analytics **math** stays in Model `ProjectCore`. Reports wires UI → `ProjectCore.*` methods, builds chart `DataTable`s, and exports via Office interop. Validation is Reports-assembled: bind scenario → `findPredictedShares()` → `rebaseProductNewShare(getTotalProductsAuditShare())` → columns `Audit Share` / `Rebased Simulator Share`.

---

## Decompiled source layout

| File | Role |
|------|------|
| `MarketSimulator.Model/ProjectCore.cs` | Main facade: share pipeline, display table, segmentation, revenue/value/penetration |
| `MarketSimulator.Model.BusinessLogic/Products.cs` | `calculateNewShare`, `rebaseNewShare` (WD, size, zero-sum rebasing) |
| `MarketSimulator.Model.BusinessLogic/Attributes.cs` | Discrete + conditional utilities, **linear interpolation** |
| `MarketSimulator.Model.BusinessLogic/Segments.cs` | Respondent → segment mapping |
| `MarketSimulator.Model.BusinessLogic/Groups.cs` | Product group rollups |
| `MarketSimulator.Model.DataAccess/DataTransaction.cs` | MDB/ADO data layer, `updateFinancial` |
| `MarketSimulator.Model.Database/DatabaseDesign.cs` | Access DB schema creation |
| `MarketSimulator.Model/Engine.cs` | Feature flags (same role as Python `model/engine.py`) |

---

## Simulation pipeline (from decompiled C#)

```
findPredictedShares()                          [ProjectCore.cs ~869]
  ├─ Temporarily exclude NewProduct SKUs
  ├─ Reset CurrentShare / CurrentPreferenceShare to base
  ├─ loadPreferenceShare("NewPreferenceShare") [~2102]
  │    ├─ loadUtilityMatrix()                  [~2024]
  │    │    ├─ getProductUtility() per respondent × product [~2060]
  │    │    │    └─ attribute.getAttributeLevelUtility() [Attributes.cs ~286]
  │    │    ├─ exp(utility × exponentFactor)
  │    │    └─ Normalize to choice probs per respondent (included SKUs only)
  │    └─ getPreferenceShare() = avg choice prob × 100 [~2087]
  ├─ product.calculateNewShare(sizeIndex)      [Products.cs ~700]
  │    ├─ Adjust pref share: × (NewWD/OriginalWD) × (Size/BaseSize)
  │    ├─ Renormalize preference shares to 100%
  │    ├─ newMs from pref share ratio × base share (product type rules)
  │    └─ rebaseNewShare() — zero-sum volume rebasing [~747]
  ├─ Handle ReplacedProduct / NewProduct edge cases
  ├─ setProductsRevenue()
  ├─ setProductsValueSalesUnit()
  ├─ setProductsValueShare()
  └─ setProductsPenetration()                  [~1553]
       Penetration = NewPreferenceShare × NewWD × (sum NewShare / sum pref×WD)
```

**Segmentation** (`respondentSegmentationProcess`, ~548): sets `respondentSelection[]` flags, then recalculates base preference shares.

**Attribute change** (`changeProductAttributeCombination`, ~600): parses `%`, `WD`, discrete levels; validates bounds; triggers `findPredictedShares()`.

---

## Key formulas (decompiled)

### Per-respondent choice probability

```
utility[i,j] = SUM_attr getAttributeLevelUtility(respondent i, product j, attr value)
expUtility[i,j] = exp(utility[i,j] × exponentFactor)
choiceProb[i,j] = expUtility[i,j] / SUM_k(expUtility[i,k])   for included k
preferenceShare[j] = AVG_i(choiceProb[i,j]) × 100               for selected respondents
```

### Conditional attribute interpolation (`Attributes.interpolateUtiltiy`)

```
U = U_next - (level_next - attr_value) × (U_next - U_prev) / (level_next - level_prev)
```

### Market share after preference change (`Products.calculateNewShare`)

For existing products:

```
NewShare = CurrentShare × (NewPreferenceShare / CurrentPreferenceShare)
```

Then `rebaseNewShare()` scales positive/negative volume deltas so total market volume is preserved (matches SOP doc zero-sum redistribution).

### Penetration (`setProductsPenetration`)

```
scale = SUM(NewShare) / SUM(NewPreferenceShare × NewWD)
Penetration[i] = NewPreferenceShare[i] × NewWD[i] × scale
```

### Value share (`setProductsValueShare`)

```
ValueShare[i] = ValueSalesUnit[i] / valueShareUnit × 100
valueShareUnit = total included VSU (from setProductsValueSalesUnit)
```

---

## Python port mapping

| C# (decompiled) | Python module | Fidelity |
|-----------------|---------------|----------|
| `loadUtilityMatrix` / `getProductUtility` | `model/preference_engine.py` | **Ported** (per-respondent MNL, vectorised NumPy) |
| `Attributes.getAttributeLevelUtility` / `interpolateUtiltiy` | `model/attribute_utilities.py` | **Ported** (discrete + conditional interpolation) |
| `findPredictedShares` | `model/share_engine.py` | **Ported** (full pipeline orchestration) |
| `Products.calculateNewShare` / `rebaseNewShare` | `model/share_calculator.py` | **Ported** (WD, size, zero-sum rebasing) |
| `changeProductAttributeCombination` | `model/attribute_engine.py` | Partial (edit rules; triggers share recalc via `ProjectCore`) |
| `setProductsRevenue` / `setProductsValueShare` | `model/metrics.py` | Ported (revenue, VSU, value share) |
| `setProductsPenetration` | `model/metrics.py` | **Ported** (`pref × WD × scale`) |
| `respondentSegmentationProcess` | `model/study_model.py` | **Ported** (`_apply_segmentation_filter` in pipeline) |
| `updateFinancial` / NOS / TDC | `model/financial.py` | Stub |
| `Groups` / `getGroupShare` | `model/groups.py` | Volume rollup only |

---

## Chart Analysis analytics APIs (Model DLL)

Full C# in `decompiled/MarketSimulator.Model/MarketSimulator.Model/ProjectCore.cs`. Spec: [`CHART_ANALYSIS.md`](../CHART_ANALYSIS.md).

| C# method | Python module | Status |
|-----------|---------------|--------|
| `SoMvsSoP` | `model/analytics/som_sop.py` | **Ported** |
| `conditionalAttributeElasticityPerProduct` (+ group/company variants) | `model/analytics/elasticity.py` | Pending |
| `priceIndexAndRevenues` | `model/analytics/price_index.py` | Pending |
| `crossBrandInteractionPerProduct` / `PerGroup` | `model/analytics/cross_brand.py` | Pending |
| `FairShareOnSKUs` / `FairShareOnBrands` | `model/analytics/fair_share.py` | Pending |
| `exIncrementalGainLoss` / `cumIncrementalGainLoss` | `model/analytics/incremental.py` | Pending |
| Validation tab | Reports DLL + audit columns | N/A (UI assembly) |

**Key corrections from decompile (not guesses):**

- **SoM vs SoP:** Y-axis = `Penetration / 100`, not Preference Share. DataTable column is `"Brand Share"`.
- **Elasticity:** `(NewShare − baseShare) / baseShare` per attribute grid level — relative share change, not arc price elasticity.
- **Fair share:** Focal-SKU rebase algorithm with `memberChange`; not simple preference-proportion fair share.

`MarketSimulator.Reports.dll` (`ChartForm`) still **not decompiled** — tab order, axis formatting, and Excel export layout only.

---

## HBU / data layout

From `StudyStructure.cs` and DLL data layer:

- RespondentID, RespondentWeight
- ProductID1..N (SKU utilities in HBU file)
- `{Attr}Level1..K` per attribute

Runtime utilities are built from **`attributeUtility`** matrices loaded from MDB/HBU, not from `ProductID*` columns alone (Python port currently ignores part-worth rebuild).

---

## Storage (Plan 1)

MDB tables replaced by `study.json` + `hbu.parquet` per study folder.

## Plan 2 (not started)

Golden reference tests against exports from the original `.exe` to validate numeric fidelity after Python re-port from decompiled C#.

---

## Related files

- Binary DLLs: `decompiled/lib/` (from `simulatorcode/simulatorC/MarketSimulator/lib`)
- Decompiled C#: `decompiled/MarketSimulator.Model/`
- API reflection dump: `decompiled_reference/ProjectCore_API.txt`
- Python math doc: `MATHEMATICAL_MODEL.md`
- WinForms study wizard (layout reference): `simulatorcode/simulatorC/MarketSimulator/StudyStructure.cs`
