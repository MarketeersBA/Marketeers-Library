# Mathematical model

Tracks: `backend/model/attribute_utilities.py`, `attribute_engine.py`, `preference_engine.py`, `share_calculator.py`, `share_engine.py`, `metrics.py`.

The simulator answers: if price, pack size, distribution, or another attribute changes, how do choices and market outcomes move? It is a what-if calculator on a conjoint model. It does not forecast category growth.

Analytics, the price search, the corridor sweep, and assortment introduction all call this same pipeline.

```text
survey utilities → preference shares → volume shares → revenue and other metrics
```

Desktop lineage for the same ideas is in [`../../legacy/`](../../legacy/README.md). That tree is not the web contract. Formulas one side computes and the other does not are in [Formula gaps](../../formula-gaps.md).

## Terms

| Term | Meaning |
|------|---------|
| Respondent | One person in the HBU file. Choice uses weight 1 unless `MARKET_SIM_USE_RESPONDENT_WEIGHT` is on. |
| HBU | Respondent-level utilities for products and attribute levels. |
| Utility | How much a respondent likes a configuration. Higher is more preferred. It can be negative. |
| Discrete attribute | A named level (brand, flavor). The level picks one utility column. |
| Conditional attribute | A number (price, size). Utility is blended between calibrated points. |
| Preference share | Stated choice among included products, before distribution and size. Included products sum to about 100%. |
| Volume share | Market outcome after distribution, size, product type, and rebasing. The modeled set sums to the market total. |
| WD | Weighted distribution, how widely the product is available. |
| MSU, SU | Study scale factors in the revenue formula. |
| VSU | Value sales unit. An anchor can tie value share to an external value total. |
| Exponent | Dial on utilities before the choice rule. Above 1 makes favorites sharper. |
| Segment | A respondent filter plus that member's base share, price, WD, MSU, and VSU when the study stored them. It does not reweight the sample to population proportions. |

## Pipeline

`ShareEngine.find_predicted_shares` / `_run_pipeline`, entered from `ProjectCore`:

1. Load utilities and blend conditional attributes for the scenario value.
2. Turn scores into preference shares (multinomial logit).
3. Turn preference into volume share (distribution, size, product type, rebase). When a new SKU is included, this step runs twice.
4. Compute revenue, value share, penetration, and indices.

## 1. Utility

For each respondent and product, included attributes add up.

A discrete attribute uses the utility of the selected level.

A conditional attribute is observed only at calibrated points. Between two points `v₀` and `v₁` with utilities `u₀` and `u₁`, a scenario value `x` gets

```text
utility = u₀ + (x − v₀) / (v₁ − v₀) · (u₁ − u₀)
```

If a value reaches the engine outside the grid, the blend continues on the first or last segment. Runner edits do not send those values while `MARKET_SIM_ENFORCE_CALIBRATED_BOUNDS` is on (the default). A `New {Attr}` outside that product's calibrated `{lo, hi}` is rejected and the cell stays unchanged. Endpoints are inclusive. WD stays on 0–100 either way. The optimizer searches inside calibrated price bounds either way.

Example: prices 10 (utility −2) and 12 (utility −4), scenario price 11 → utility −3.

Total score for the choice step is the sum of attribute utilities. When respondent weights are on, that score is multiplied by the HBU `RespondentWeight` (an `RLH` or `Weight` header is stored in that column) before `exp`. The default is off: every person has weight 1, matching the desktop Excel import. The preference-share average is still an unweighted mean over the selected respondents.

## 2. Preference share

`preference_engine.compute_preference_shares`.

For each person and each included product:

```text
probability = exp(exponent × score) / Σ exp(exponent × score of included products)
```

People outside the active segment contribute nothing. Choosing a member also replaces market inputs before the choice step. A column named for that member supplies base share, base price, current price, base WD, and current WD. `(Member) MSU` and `(Member) VSU` on the Description sheet replace the study scale and the value anchor. Respondent code 1 is the first level name on the Segments sheet, so code `1` and the name `Jaddah` are the same member when `Jaddah` is listed first. A missing column leaves that input on the total-market value. Clearing the segment restores those totals. The sample is not reweighted to population.

Preference share is

```text
100 × (sum of that product's probabilities) / (number of people in the segment)
```

Business reading: the fraction of choosers who would pick the SKU before distribution and pack size are applied.

This is multinomial logit. Relative odds between two products do not change when a third is added (independence of irrelevant alternatives). That property is inherited from the choice rule.

## 3. Volume share

`share_calculator.calculate_new_shares`.

Availability and size adjust preference first.

Existing products:

```text
adjusted preference = preference × (new WD / base WD) × (new size / base size)
```

New products skip the WD ratio at this step (WD enters the launch formula below) and still apply the size ratio. Adjusted preferences of included products are rescaled to 100.

Volume then depends on product type:

| Type | Formula |
|------|---------|
| Existing | `current volume share × (new preference / current preference)` |
| Replaced | `replaced SKU's volume share × (replacement preference / replaced preference)` |
| New | `preference × WD × consumption calibration × SOR × awareness × repeat / 100`, then promotion calibration |

Example, existing: preference 10% higher than current (`new / current = 1.10`) raises volume share 10% before rebasing.

Example, replaced: old SKU volume 8 and preference 10, replacement preference 12 → `8 × (12 / 10) = 9.6`.

Rebase keeps the market total fixed. Gains and losses are scaled so the net change versus the reference total is zero, then

```text
volume share = volume share × (total current volume / total new volume)
```

Business reading: one SKU's gain is another's loss. A launch redistributes the modeled market. It does not create a 110% category.

### Two passes when a new SKU is included

`apply_volume_passes`. This matches desktop `findPredictedShares`.

If no new SKU is included, preference and volume run once. Current share stays the base share.

If a new SKU is included:

1. Hold every included new SKU out of the lineup.
2. Compute preference and volume for the remaining SKUs. A price change and a replacement settle here, before the launch.
3. Copy that pass onto current share. A replacing SKU writes its new share and new preference onto the base SKU. Every other included SKU copies its own. An excluded SKU whose replacements are all out gets current share 0. If a replacement is in, the base keeps the share written above.
4. Put the new SKUs back and compute preference and volume again. Existing and replacing SKUs then move only by how the launch changed preference, starting from the market in step 2. The new SKU takes its launch volume against that market.
5. Restore the new SKUs to the lineup if a pass fails.

Revenue and the other metrics run once, after the last pass.

### Who is in the lineup

`AttributeEngine.include_exclude` follows the desktop include rule.

A study SKU is one of three types, from base share and `BaseProduct`:

| Type | Rule | Starts |
|------|------|--------|
| Existing | Base share ≠ 0 and no base product | Included |
| Replacing | Base share = 0 and `BaseProduct` points at another SKU | Excluded |
| New | Base share = 0 and no base product | Excluded |

Including a replacing SKU selects it instead of the base SKU and instead of the other replacements of that base. Including the base SKU drops its replacements. Including a new product adds it and leaves everyone else as they are. Excluding a SKU only turns that SKU off. It does not put the other variant back.

On the Runner grid a replacing SKU is not its own row. The base SKU's name cell is a menu of that SKU plus its replacements. The label is the included one, and the row's price, distribution, and shares are that SKU's. The checkbox includes or excludes the SKU the menu is showing. New products stay on their own rows and are added from the group menu.

Results rows carry `ProductType` and `BaseProduct` (the base SKU's name, empty when this SKU replaces nobody). The grid does not show those columns.

## 4. Revenue, value, penetration

`metrics.apply_scenario_metrics`.

```text
revenue = MSU × SU × volume share × price / (pack size × 100)
```

Base revenue uses base share, base price, and base pack size. Scenario revenue uses the scenario pack size.

The desktop app kept the original pack size in the revenue and VSU denominator after a size-only edit, while volume share already used the new size. This app divides by the scenario pack size. When pack size is unchanged, the two match. When only size changes, revenue and VSU differ from the desktop by about `base size / scenario size`.

```text
VSU = MSU × volume share × price / (100 × pack size)

value share = VSU / value share unit × 100
```

The value-share unit is the sum of included VSU, plus the study VSU anchor, minus the sum of base revenue for the included base products. The anchor ties value share to an external value total when the study has one.

Value share weights products by money. A small premium SKU can have a high value share.

Penetration lines preference and distribution up with volume:

```text
scale = sum(volume share) / sum(preference share × WD)
penetration = preference share × WD × scale
```

Indices:

```text
S.Index = volume share / base volume share
R.Index = revenue / base revenue
```

Above 1 is up versus base.

## What this does not mean

The pipeline does not add category growth, and it does not reweight a segment to population. Out-of-grid conditional values are a straight-line extension only when the edit is allowed into the engine. Company money (NOS, TDC, company MM$) is a separate stored-input formula in [Measures](measures.md).
