# Pricing strategy

Tracks: `backend/model/strategy/` (`probe.py`, `diagnosis.py`, `roles.py`, `position.py`, `plan.py`, `settings.py`) and the client deck sections in `backend/report/sections/client/`.

The client strategy deck in the [study report](report.md) is built from this analysis. Every number reruns the [mathematical model](mathematical-model.md) with a few prices changed. Nothing is fitted separately from it. The analysis runs on the base case, total market, with no segment filter.

## Business question

The simulator answers "what if?", but a client rarely knows which ifs are worth asking. The analysis asks them on the client's behalf:

- Who are our real competitors? This is not always the biggest brands.
- What is each important SKU's job in the portfolio?
- Where should each SKU sit against its real rival, and should it follow that rival's price cuts?
- Which prices should move for the chosen goal, what does each move bring, and does the plan survive a competitor attack?

## Steps

```text
diagnose (±5% probes) → roles → price position (gap sweep, follow or hold)
  → plan limits → optimizer, one run per goal → breakdown and stress test
```

### Probes

`Market` (in `probe.py`) loads the study at base prices. Each probe sets some SKU prices, reruns the optimizer's fast pipeline (`_set_prices`), and reads volume share, value share, revenue and preference share for every included SKU. Prices not named in a probe stay at base. Own-portfolio totals sum the company's included SKUs. Margin is revenue × (p − c) / p, and is defined only when every own SKU has a unit cost.

### Options

The export dialog sets these for one deck (`StrategyOptions` in `__init__.py`). Everything else is a fixed threshold.

| Option | Default | What it changes |
|--------|---------|-----------------|
| Goal | Revenue | The objective of price position, follow or hold, and the plan |
| Brands | All own brands | Which own SKUs are candidates for important SKUs, so which are assessed and repriced |
| Max price change | 10% | `plan_max_change` |
| Max volume loss | 3% | `plan_volume_drop_pct`: the plan's cap and the price-position eligibility |

With a brand subset, objectives and "our" totals still sum every own SKU. A move in a covered brand can't win by taking volume from another own brand. Own overlap and threat also count every own SKU. An SKU's share of our value is still a share of all own value. There is no segment option.

### Important SKUs

Own standalone SKUs of the covered brands (offer-type filter as in the optimizer), sorted by value share, are taken until they cover 80% of those brands' value share. The SKU that crosses the line is included. "Large" SKUs are the ones taken before 50%. At most 15 SKUs are assessed. Everything else is the **tail**, shown as a group and not assessed one by one.

### Measures per important SKU

| Measure | Procedure |
|---------|-----------|
| Price sensitivity E | The SKU alone at +5% and at −5%. E = \|Δ volume / volume\| / 5%, averaged. If the two sides differ by more than 2×, the SKU is flagged "uneven price response" and the larger figure is used. |
| Own overlap C | In the −5% probe, the volume the SKU wins is split by the SKUs that lose it. C is the part lost by our other SKUs. |
| Fair own share | Our other SKUs' volume ÷ the market's volume excluding this SKU. This is what C would be with no special overlap. |
| Diversion | The rest of the gain, split by competitor brand. It sums to 1 over competitors. |
| Real rival | The competitor brand with the largest diversion, if it supplies at least 20%. Otherwise the SKU has none. |
| Closest SKU | The rival's SKU that loses the most. |
| Price index | Our price ÷ the closest SKU's price × 100. Per unit when the pack sizes differ by more than 5%, otherwise per pack. |
| Preference gap | Preference share rescaled to the market's volume total, minus volume share, in pp. |

### Competitor threat

Each competitor brand (the 25 largest by volume share) cuts every one of its SKUs 5%:

| Measure | Definition |
|---------|------------|
| Threat | Our total volume share lost, as % of our volume |
| Threat to large SKUs | The same, over our large SKUs only |
| We take | The share of what our important SKUs win from competitors (on their −5% probes) that comes from this brand |
| Threat index | (brand's share of total threat) ÷ (brand's share of tested competitor volume). 1 = in line with its size |
| Diversion index | The same for "we take" |

A brand is a **hidden threat** when it ranks in the top 5 by threat with a threat index ≥ 1.5. A brand is a **distant giant** when it is top 3 by share with both indices ≤ 0.67. The **main threat** is the brand with the largest threat to our large SKUs.

### Roles

The first matching rule wins. Sensitivity is compared with the median E of the important SKUs, so the rules work across categories.

| # | Role | Rule |
|---|------|------|
| 1 | Profit engine | Large, and E ≤ median |
| 2 | Cannibal | C ≥ 50% and C ≥ 1.25 × fair own share |
| 3 | Shield | Not large, and ≥ 30% of its diversion comes from the main threat |
| 4 | Fighter | E ≥ median |
| 5 | Premium | Price index ≥ 115 and E ≤ 0.8 × median |
| 6 | Hidden potential | Preference gap ≥ 0.5 pp and rescaled preference ≥ 1.3 × volume share |
| 7 | Core | None of the above |

Flags are added after the role:

- "overlaps with X": C ≥ 30% and above 1.25 × fair share, unless the SKU is already a cannibal;
- "hidden potential", unless that is the role;
- "margin below 20%", when costs exist;
- "uneven price response".

The cannibal test is relative to fair share because a company that holds most of the market takes most of any gain from itself. A raw 60% overlap is normal at 55% share and alarming at 30%.

### Price position

For each SKU with a real rival, `gap_curve` moves only our price, ±15% around today, in 21 steps plus today. The range is clipped to the SKU's calibrated price levels (`get_calibrated_bounds`). The rival and all other prices stay fixed. Each point records the SKU's volume and the own-portfolio objective.

`read_curve`:

- **Eligible.** For revenue and margin, a point that loses more than the max volume loss (default 3%) of our total volume is not eligible as best. This is the plan's volume cap. Today is always eligible.
- **Best.** The eligible index with the highest objective.
- **Band.** Contiguous eligible indices within 0.5% of the best value. "Keep within a–b% of the rival."
- **Cliff.** The first step at or above the best index where volume falls at least 1.5× faster than the median step. The band is capped at the cliff.
- **Flat.** The objective moves less than 0.5% across the range. The deck then says the gap barely matters.
- **Edge.** The best is the first or last point. The deck adds "a higher (lower) price still helps".

### Follow or hold

The rival brand cuts every SKU 5%. We then hold our SKU's price, cut it 2.5%, or cut it 5%. The deck recommends the best response only if it beats holding by at least 0.1% of the objective. Otherwise it says hold.

### Plan

`plan_limits` sets each important SKU's price range:

- the calibrated levels, within the max price change of today (default ±10%);
- no cuts for premium and cannibal SKUs;
- the band from the price position, converted back to a price.

The range always contains today's price.

`optimise` runs `optimize_coordinate_ascent` ([Price search](optimizer.md)) over those SKUs:

- the objective is summed over all own included SKUs;
- the start is `multi_start="current"`, with up to 6 passes;
- revenue and margin plans may lose at most the max volume loss (default 3%) of our volume.

One plan runs per goal (revenue, margin when costs exist, value share). Their totals make the trade-off chart. A price that moves less than 0.5% is treated as unchanged. If the chosen plan's gain is below 0.5%, the deck says today's prices are near the best and shows no moves.

### Breakdown and stress test

- **Breakdown.** Each plan move is simulated on its own. Its effect is the change in the objective. "Moves together" is the total gain minus the sum of the single moves.
- **Stress test.** The 3 competitors with the highest threat cut every SKU 5%. This is run with today's prices and with the plan. Kept gain = (plan − today) under attack ÷ (plan − today) without.
- **What not to do.** −5% probes where the SKU's own volume rises but the objective falls, sorted by loss.

## Limits

- **Model view.** Diversion and threat are the choice model's view of switching, not audited switching. The trust slide shows validation waves when the study has them.
- **No competitor reaction.** Probes and the plan hold competitor prices fixed, except in the follow-or-hold and stress tests.
- **Thresholds.** The thresholds are judgement calls, kept in one place (`StrategySettings`) and printed in the deck's appendix. Change this page with them.
- **Scope.** Total market only. The client deck offers no segment option.
