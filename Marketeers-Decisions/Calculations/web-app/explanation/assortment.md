# Assortment

Tracks: `frontend/src/components/assortment/cumulativeIntroUtils.ts`, `frontend/src/pages/AssortmentPage.tsx`, `backend/model/analytics/incremental.py`.

The screen is `/assortment`. The introduction statistic is [Analytics](analytics.md). The query string is [Assortment query](../reference/assortment.md).

## Business question

In this group, which SKUs earn a place in the lineup, which are weak, and which sell well for their distribution and should get more WD before the range grows further?

## Statistic

The server runs cumulative introduction with value share and `intro_order=sorted`, and the page sends an empty starting lineup. That combination does not re-simulate the market after each add. It ranks the SKUs by value share in the loaded scenario, highest first, and accumulates those shares.

Each step's jump is that SKU's own value share, in percentage points:

```text
jump = this SKU's scenario value share
```

The cumulative series is the running sum of those jumps. Because the shares are not recomputed on a growing lineup, a later SKU's jump does not depend on the SKUs already listed.

Product Sales Rate uses that jump and the SKU's WD (percent):

```text
PSR = value share (percentage points) / WD (percent)
```

WD comes from the introduction row, or from the session's `New WD` when the row has none. PSR is null when WD or the jump is missing or not positive.

The anchor is step 1: the SKU with the highest scenario value share in the set.

## Procedure

1. Call cumulative introduction for the selected group and the SKUs in view.
2. Attach PSR.
3. Flag Push WD when PSR is at least the median PSR of the steps and WD is more than 10 percentage points below the anchor's WD.
4. Label each step:

| Label | Rule, first match wins |
|-------|------------------------|
| Watch | The introduction row has an error, or the jump is not above 0.005, or WD is missing, or PSR is missing, or PSR is below the median |
| Push WD | The flag in step 3 is set |
| Keep | Step 1 (the anchor), or any later step that cleared the checks above |

The headline under the chart is built from this list. If any step is Push WD, the headline adds that distribution should be raised on those SKUs before lower-efficiency items are expanded.

## What this does not mean

The labels are not a field on `incremental_gain_loss`. Another client that only calls the analytics route will not receive Keep, Watch, or Push WD.

The order is descending scenario value share. It is not an optimized subset, and a SKU's jump does not change because of the SKUs above it. A different `incremental_type` or `intro_order` would re-simulate; this screen does not send those.

The own-brand efficiency panel is not on the page. The component exists and is not rendered.

Push WD does not change WD in the study. It is a recommendation on the screen.
