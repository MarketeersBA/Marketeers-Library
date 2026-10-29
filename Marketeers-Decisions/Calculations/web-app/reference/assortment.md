# Assortment query

Tracks: `frontend/src/components/assortment/cumulativeIntroUtils.ts` (`buildIntroQueryParams`), `backend/api/routes/analytics.py`.

What the labels mean: [Assortment](../explanation/assortment.md).

The page calls:

```http
GET /api/studies/{slug}/analytics/introduction
```

| Query | Value the page sends |
|-------|----------------------|
| `mode` | `cumulative` |
| `incremental_type` | `Value Share` |
| `intro_order` | `sorted` |
| `group_type` | The selected group type |
| `group_member` | The selected member, omitted when the type is `Company Products` |
| `products` | Comma-separated SKU names in view |
| `current_lineup` | A single space, which the API trims to an empty lineup |
| `filters` or segment pair | The workspace segment filters, when any are set |
| `base_scenario` | The workspace base scenario, when one is set |

That combination ranks SKUs by scenario value share and accumulates those shares. It does not re-simulate a growing lineup. See the introduction section of [Analytics](../explanation/analytics.md).

The response rows include `productName`, `valueShare` (cumulative, as a fraction), `incrementalDelta`, `distribution`, and `valueSales`. The page turns `valueShare × 100` into a cumulative percent and uses the step-to-step difference as the jump.

Keep, Watch, and Push WD are computed in the browser after this response. They are not query parameters and they are not response fields.
