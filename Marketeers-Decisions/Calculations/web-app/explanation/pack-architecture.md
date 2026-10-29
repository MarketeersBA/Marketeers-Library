# Pack architecture

Tracks: `backend/model/optimizer.py` (`_ppa_ladder_ok`, `_get_blocks`). The screen is `/price-pack-architecture`. It calls the same `POST /run` as Price Optimization with `enforce_ppa_ladder` on when it has size chains or tier links.

The search procedure is in [Price search](optimizer.md).

## Business question

Can this brand's packs stay in a coherent ladder while prices are optimized: a larger pack costs more in absolute money, and costs less per unit, within an optional saving band?

## Statistic

Along a size chain ordered small to large, for neighbors `a` then `b` with prices `p` and sizes `s`:

```text
p_b > p_a
p_b / s_b ≤ p_a / s_a
```

Optional saving band, as a fraction of the smaller pack's unit price (`ppa_min_saving`, `ppa_max_saving`):

```text
unit_b ≤ unit_a × (1 − min saving)
unit_b ≥ unit_a × (1 − max saving)
```

A tier link between a high SKU and a low SKU requires the high SKU's unit price to clear the low SKU's unit price by `min_premium_pct`:

```text
unit_high > unit_low × (1 + min premium)
```

Missing sizes on an active chain fail the check. The ladder is not skipped when a size is absent.

## Procedure

The ladder is off unless `enforce_ppa_ladder` is true. Price Optimization leaves it false. Price-Pack Architecture turns it on when the request carries chains or tier links.

`_get_blocks` then groups the search:

1. Reference SKUs named by tier links, when more than one of them is a decision SKU, form one block.
2. Each size chain with two or more decision SKUs is its own block.
3. Every other decision SKU is a block of one.

A multi-SKU block is enumerated jointly on a shortened menu. A combination that fails the ladder is not sent to the simulator. The diagnostics `ppa_ladder_rows` report price, €/unit, saving, and pass or fail for the prices that were returned.

## What this does not mean

The ladder does not set the prices by itself. It rejects price vectors. A long chain is not an exhaustive grid of every menu price: the block menu is subsampled (fewer points as the chain gets longer) before enumeration.

Turning the ladder on does not change the demand model. Share, revenue, and margin are still the simulator pipeline.
