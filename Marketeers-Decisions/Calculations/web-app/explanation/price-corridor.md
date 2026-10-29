# Price corridor

Tracks: `backend/model/corridor_preview.py`. The Price Optimization chart calls `POST /api/{slug}/optimizer/corridor-preview`.

Units for margin and volume drop are in [Measures](measures.md). How the run uses the band is in [Price search](optimizer.md).

## Business question

If only the focus reference SKU's pack price moves, and every other focus SKU stays at today's price, where do contribution and volume sit against a competitor anchor, and which prices still clear the margin floor and the brand volume-drop cap?

## Statistic

The horizontal index is pack-price parity with the anchor:

```text
index = reference pack price / anchor pack price × 100
```

Index 100 means the same pack price, not the same price per litre or per kilogram. The euro ticks are the reference SKU's calibrated price window. Changing the anchor changes the index under each euro. It does not move the euro labels.

At each price the pipeline is re-run. The chart has:

| Series | Definition |
|--------|------------|
| Reference volume drop | `(base − new) / base × 100` on the reference SKU's volume share |
| Brand volume drop | The same on the sum of the focus brand |
| Reference CM% | `(p − c) / p × 100` |
| Brand CM% | Brand contribution / brand revenue × 100 |
| Reference contribution | Money contribution of the reference SKU |

Default grid: 10 evenly spaced indexes across the calibrated lo/hi. `index_step` switches to a fixed step. `index_min` and `index_max` force a window. Evaluated prices are clamped to the calibrated bounds. Today's shelf price is added if the grid missed it, so indexes versus today can meet at the current price.

## Procedure

Only the reference SKU's price changes. The anchor price is an input. The preview does not look the anchor up. The page chooses the anchor SKU as the highest volume-share SKU in the selected competitor brand and sends that SKU's price.

A point is feasible when the reference SKU's unit CM% is at least `min_margin_pct` and the brand volume drop is at most `max_volume_drop_pct`. Both are percent points, the same units as the sliders.

The bottom of the band is the higher of the calibrated floor and the price that just clears the margin floor, rounded up to the cent. The top is the calibrated ceiling, or the last price that still passes the volume cap if the ceiling fails it. The recommended price is the point in that band with the highest reference-SKU contribution in money, found by a cent-scale scan because the contribution curve is jagged.

If no price is feasible, the response still marks a least-bad point (smallest margin and volume violation, then higher reference contribution) and labels the band infeasible.

Missing unit costs reject the preview when a margin floor is set. Costs are the stored study costs.

## What this does not mean

The sweep is not the multi-SKU search. Other focus SKUs do not move. The index is not € per unit size. `corridor_basis: "unit"` exists on `POST /run` only.

The preview does not itself constrain the optimizer. The Price Optimization page copies the band's minimum and maximum pack prices into `corridor_min` and `corridor_max` as ratios (`price / anchor price`) on the run.

Brand CM% on the chart is not the margin constraint. The constraint is the reference SKU's unit CM%.
