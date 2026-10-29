# Introduction and removal

Source: `ProjectCore.exIncrementalGainLoss`, `cumIncrementalGainLoss`, `includeProductsToCurrentLineUp`, `excludeProductsFromCurrentLineUp`, `getHightestIncrementalProduct`, `ChartForm` Introduction and Removal tabs, `IncrementalConfiguration`.

Introduction adds products. Removal takes them out. Both measure the same two outcomes on a lineup: revenue and volume share of a group member, or of company products when the group type is `Company Products`.

The configuration grid has two columns. Introduction: `Current Line Up Products` and `Products To Be Added`. Removal: `Current Line Up Products` and `Products To Be Removed`. Non-empty cells become the lineup list and the candidate list.

`setCurrentMarketWithLineUpProducts` sets `Included` from the lineup, and only inside that group member or inside the company-product list. Products outside the set are left as they were.

`getScenarioValue` runs `findPredictedShares` and returns group or company revenue, or group or company new share. A failure while testing one product (the utility throw `{product} has a base level out of boundry values` is one) drops that product from the result. The row then says `Reversal Effect Happened` and is omitted from the chart.

## Exclusive

`exIncrementalGainLoss`. The current lineup is scored once (`Current` row). Each candidate is then toggled alone — included for Intro, excluded for Removal — scored, and toggled back.

| Column | Value |
|--------|--------|
| Revenue | Level after the toggle, rounded to 4 decimals |
| Market Share | That share / 100, rounded to 4 decimals |
| Revenue Intro or Revenue Removal | `round(revenue - currentRevenue, 4)` |
| Market Share Intro or Market Share Removal | `round(share - currentShare, 4) / 100` |

Chart name `EX Intro` or `EX Removal`.

## Cumulative

`cumIncrementalGainLoss` repeats until the candidate list is empty. Each step scores every remaining candidate with a one-product toggle and keeps the one with the largest `value - base`. `value` and `base` are revenue or share, whichever box is being run (`Market Share` or `Revenue`). A tie tries each tied product and keeps the one whose following step has the larger next delta.

Intro appends the winner to the lineup. Removal removes it. The lineup is applied, and the new revenue or share becomes the base for the next step. If every candidate fails, the rest of the list is marked `Reversal Effect Happened`.

The current row and each accepted product store revenue and market share (share column divided by 100). The step column `{Revenue|Market Share} {Intro|Removal}` stores the winning delta in raw units: revenue, or share on the 0–100 scale, not divided by 100.

Chart names: `CUM Intro Market Share`, `CUM Intro Revenue`, `CUM Removal Market Share`, `CUM Removal Revenue`. The screen chart can run share and revenue as two tables when both boxes are checked. Revenue is the secondary axis. Market share is a percent.

On the Excel chart, rows at the bottom whose revenue cell is `Reversal Effect Happened` are left off the plot. See [EXPORT.md](../EXPORT.md).
