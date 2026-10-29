# Market Simulator Web App Manual

This folder describes the web app that runs in this repository. A behavior change updates the matching page in the same commit. Each page names the code it tracks.

Specifications for the legacy desktop simulator are organized in [`../legacy/`](../legacy/README.md). Older briefs and notes are in [`../../notes/special-purpose/`](../../notes/special-purpose/). Differences between formulas are detailed in [Formula gaps](../formula-gaps.md).

## How the pages are split

| Kind | Question |
|------|----------|
| Getting started | How do I run it? |
| Explanation | What does this number mean, and why does the procedure work? |
| Reference | What do I send, and in which units? |
| Operations | How do I release to AWS? |

## Start here

| I want to… | Page |
|------------|------|
| Install and run | [Getting started](getting-started.md) |
| Understand a share or a revenue figure | [Mathematical model](explanation/mathematical-model.md) |
| See which formulas are desktop-only or web-only | [Formula gaps](../formula-gaps.md) |
| Understand margin, volume drop, indices, NOS/TDC | [Measures](explanation/measures.md) |
| Understand a price recommendation | [Optimizer](explanation/optimizer.md) |
| Understand the corridor chart | [Price corridor](explanation/price-corridor.md) |
| Understand a pack ladder | [Pack architecture](explanation/pack-architecture.md) |
| Understand SoM/SoP, switching, or intro/removal | [Analytics](explanation/analytics.md) |
| Understand Keep / Watch / Push WD | [Assortment](explanation/assortment.md) |
| Understand the PowerPoint export (analyst and client decks) | [Study report](explanation/report.md) |
| Understand real competitors, SKU roles, price rules and the recommended plan | [Pricing strategy](explanation/strategy.md) |
| Look up a route or a field | [HTTP](reference/http.md), [Optimizer fields](reference/optimizer.md), [Assortment query](reference/assortment.md) |
| Look up environment variables | [Configuration](reference/configuration.md) |
| Look up study files and validation uploads | [Studies and files](reference/studies-and-files.md) |
| Release the UI or the API | [Deployment](operations/deployment.md) |

## Screens

| Path | Section |
|------|---------|
| `/optimizer` | Price Optimization |
| `/runner` | Price Simulation |
| `/price-pack-architecture` | Price-Pack Architecture |
| `/assortment` | Assortment |
| `/data-hub` | Data Hub & Trust |
| `/wizard` | Study Wizard |
| `/studies/:slug/analytics` | Chart Analysis |
| `/settings` | Browser-local settings |
| `/login`, `/signup`, `/confirm` | Used when auth is `mock` or `cognito` |
