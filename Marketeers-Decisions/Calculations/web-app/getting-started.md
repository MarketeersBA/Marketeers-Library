# Getting started

Tracks: repository [`README.md`](../README.md), `backend/scripts/build_spa.ps1`, `backend/scripts/build_spa.sh`, `frontend/vite.config.ts`.

## Requirements

- Python 3.12 or newer (`backend/.python-version`, `requires-python` in `backend/pyproject.toml`)
- Node.js with npm

## Install

From the repository root:

```powershell
py -3 -m venv .venv
.venv\Scripts\activate
pip install -r backend\requirements.txt
cd frontend
npm install
```

`py -3` is the Windows launcher. After the venv is activated, `python` is that environment. If `python` already runs 3.12 or newer, `python -m venv .venv` is the same first step. The bare command `python` on a new Windows install is the Microsoft Store alias and does not create the venv.

## Develop

Two processes. Vite proxies `/api` to the API.

```powershell
# Terminal 1 — API, venv active
cd backend
uvicorn api.main:app --reload --port 8000

# Terminal 2
cd frontend
npm run dev
```

Open http://localhost:5173. If the API is not on port 8000, set `VITE_API_PORT` in `frontend/.env`.

Auth defaults to `MARKET_SIM_AUTH=dev` (no login page). Variables: [Configuration](reference/configuration.md).

## One process

The API can serve the SPA itself from `backend/api/spa_dist`. This is a local convenience; production serves the UI from CloudFront ([Deployment](operations/deployment.md)). `npm run build` alone writes `frontend/dist` with the production API address, which the API does not mount. The scripts below build with `--mode same-origin` instead.

From the repository root:

```powershell
powershell -ExecutionPolicy Bypass -File backend/scripts/build_spa.ps1
cd backend
uvicorn api.main:app --port 8000
```

Linux or macOS: `bash backend/scripts/build_spa.sh`, then the same `uvicorn` command from `backend/`.

Open http://localhost:8000. Without `backend/api/spa_dist/index.html`, `/` is JSON and `/docs` is the API schema. `/api/health` returns `{"status":"ok"}`.

The Windows script stops if `npm run build` exits non-zero, and it does not copy an older `frontend/dist` after that failure. The shell script stops on the same failure (`set -e`).

## Smoke test

```powershell
cd backend
python -m scripts.smoke_study
```

Creates a minimal demo study when none exists, runs a price change, and checks that shares sum to about 100%.
