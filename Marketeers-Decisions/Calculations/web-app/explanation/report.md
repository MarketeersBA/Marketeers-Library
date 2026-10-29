# Study report

Tracks: `backend/report/` (`builder.py`, `spec.py`, `pptx_renderer.py`, `sections/`), `backend/api/routes/reports.py`, `backend/scripts/build_report_template.py`, and `frontend/src/shell/ExportReportButton.tsx`.

**Export report** on every workspace page downloads a PowerPoint deck of the whole study. Charts are native PowerPoint charts, so their data, colours and labels stay editable. Every slide has speaker notes that say how its numbers are calculated.

## Two decks

| | Analyst working deck | Client strategy deck |
|---|---|---|
| Reader | The analyst, before talking to the client | The client |
| Content | Every section, saved scenario and table | Strategy: real competitors, SKU roles, price rules, a recommended plan |
| Titles | Plain ("Share by Brand") | Insight titles built from the numbers ("Shurooq SF hurts us 1.8× more than its 1.6% share suggests") |
| Input | Sections, parts, scenarios and SKUs to test | Sections, parts, the **goal**, the **plan limits** and the **brands** to cover |
| Built from | Saved scenarios and the analytics pages | The [pricing strategy](strategy.md) analysis, run at export |
| Time | Seconds | Up to a minute on large studies |

The client deck goes to the client without an analyst review. So every sentence comes from the simulator, and a slide is left out when the data can't support it:

- no rival is found;
- no validation waves exist;
- the plan's gain is below 0.5%: the deck then says today's prices are near the best.

## Client deck sections

| Section | Slides |
|---------|--------|
| Executive summary | Headline KPIs for the plan; strategic recommendations |
| Where we stand | Share by brand; price per unit vs volume share |
| Your real competitors | Threat chart; share rank vs threat rank table; where each SKU's extra volume comes from (100% stacked bars) |
| Portfolio roles | Bubble map (value share × price sensitivity, size = revenue, colour = role); role table with pricing meaning and flags |
| Price position rules | Rules table; gap curve per SKU (up to 8) with today, best and cliff markers; follow-or-hold chart |
| Recommended plan | Goal trade-off; price changes and price list; waterfall of the gain by move |
| Risks | Plan under attack by the top 3 threats; price cuts that look like growth but lose |
| Can you trust this? | Predicted vs audited share, only when validation waves exist |
| Next steps | What to try in Price Simulation and Price Optimization |
| Method | Definitions, role rules, thresholds, measures per SKU |

## Client deck settings

| Setting | Choices | Default | Effect |
|---------|---------|---------|--------|
| Goal | Revenue, margin, value share, volume share | Revenue | What the price rules, the plan and the follow-or-hold tests maximise. Margin is offered only when every own SKU has a unit cost (Data Hub & Trust, unit costs upload). |
| Max price change | ±5, 10, 15, 20% | ±10% | The largest change the plan may make to one SKU's price |
| Max volume loss | 1, 3, 5, 10% | 3% | The largest loss of our total volume a plan, or a price rule, may accept. Shown for revenue and margin only. |
| Brands to cover | The company's own brands | All | Which own brands are assessed and repriced. Shown when the study has more than one own brand. |

There is no segment setting: the client deck always covers the total market. The deck's summary lists the brands when only some are covered, and the Method section prints every setting.

If the API answers without the client settings, the dialog says it is running an older version and must be restarted, instead of loading forever.

## How it is built

The builder turns a study into a `ReportSpec`: plain dataclasses, one per slide. The renderer turns the spec into slides on `report/templates/marketeers.pptx`. Content decisions live in the sections and layout decisions live in the renderer.

Slide types:

- title, section, text, KPI and table slides;
- clustered and 100% stacked bars, line, scatter and bubble charts;
- XY lines with labelled markers;
- waterfall, drawn as a stacked column chart with a hidden base series so that it stays editable.

Insight titles longer than one line are set smaller rather than wrapped.

Regenerate the template after changing its script:

```bash
cd backend && python -m scripts.build_report_template
```

HTTP fields: [HTTP](../reference/http.md#study-report).
