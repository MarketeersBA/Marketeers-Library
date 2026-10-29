# Measures

Tracks: `backend/model/metrics.py`, `backend/model/groups.py`, `backend/model/financial.py`, `backend/model/corridor_preview.py`, `backend/model/optimizer.py` (`_compute_obj`, `_vol_drop_pct`).

Names below are the ones the screens and the search use. Formulas for preference and volume share are in the [mathematical model](mathematical-model.md).

## Volume share and value share

Volume share (`N.Share`) is units in the modeled market after rebasing. Value share weights those units by price through VSU. A price increase can raise value share while volume share falls.

`B.Share` is the base volume share. `S.Index` and `R.Index` are scenario divided by base (1 means unchanged).

Group value share uses the same denominator as SKU value share, and adds the group's excluded sales before dividing:

```text
group value share = (group value-sales units + excluded sales unit) × 100 / valueShareUnit
```

Group value-sales units are the included member SKUs. Excluded sales are the active segment member's column, or `Total` when that column is absent. A group member missing from the sheet adds 0. With excluded sales at 0 the result equals the sum of those members' value shares.

## Revenue and contribution

Revenue is the scaled money amount:

```text
revenue = MSU × SU × volume share × price / (pack size × 100)
```

Unit contribution margin on a SKU is the share of price that is not unit cost:

```text
CM% = (p − c) / p × 100
```

`c` is the stored study unit cost. A missing cost is not replaced with a fraction of price. Margin math refuses to run without it.

Brand CM% on the corridor chart is contribution divided by revenue across the focus brand:

```text
brand CM% = Σ contribution / Σ revenue × 100
```

Reference-SKU contribution in money is what the corridor uses to pick a recommended price inside the feasible band. That is not the same object as brand CM%.

The optimizer objective `margin` sums `revenue × (p − c) / p` over the scope SKUs. The margin floor on a run is the same unit ratio, applied as a minimum price `c / (1 − margin)` and as a reject on any trial below it. On the wire the floor is percent points (`34` means 34%). Inside `OptSpec` it is the fraction `0.34`.

## Volume drop

Volume drop is a percent of the starting volume, not a gap in percentage points of share.

```text
drop% = (baseline − new) / baseline × 100
```

Positive means volume was lost. Negative means volume was gained.

On a price run, baseline and new are the scope's volume share (the brand, or the included decision SKUs). The cap is `max_volume_drop_pct`. A trial above the cap is infeasible.

On the corridor chart the same definition is applied to the focus brand's volume share (`brand_volume_drop_pct`) and, separately, to the reference SKU. The guardrail uses the brand figure.

## Company MM$

When the study is financial, NOS and TDC are stored inputs on each SKU. They are not computed from price or from revenue.

Company MM$ is the only financial total the Runner shows. It is not a column on SKU or group rows.

```text
company MM$ = round( Σ (NOS − TDC) × volume share × MSU / 100 )
```

The sum is included company SKUs. Volume share is in percent (0–100). `round` is half-to-even.

This is not `revenue − NOS − TDC`.

## What these measures do not mean

A higher CM% from raising price is not a higher contribution in money: the corridor can show CM% rising while reference-SKU contribution has a peak and then falls. A volume-drop cap of 10 does not mean "share may fall 10 percentage points." It means the scope may lose 10% of its starting volume share.
