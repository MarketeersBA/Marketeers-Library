# Deployment

Tracks: `frontend/src/api/client.ts`, `frontend/.env.production`, `backend/api/main.py`, `backend/config.py`, `backend/.env.example`, `backend/scripts/build_spa.ps1`, `backend/scripts/build_spa.sh`.

Install and the dev loop: [Getting started](../getting-started.md). Variables: [Configuration](../reference/configuration.md). The step-by-step AWS records (console settings, resource inventory, troubleshooting) are in [`docs/deployment/`](../../deployment/README.md).

## Production topology (AWS, `eu-west-3`)

The UI and the API are on different hostnames.

| Layer | Service | Address |
|-------|---------|---------|
| UI | Private S3 bucket `marketeers-decisions-frontend` behind CloudFront (OAC) | `https://decisions.marketeers-ba.com` |
| API | ALB (HTTPS 443, HTTP 80 redirects) → target group HTTP 8000 → one EC2 instance, `uvicorn` under systemd unit `marketeers-decisions` | `https://api-decisions.marketeers-ba.com` |
| Catalog | RDS PostgreSQL `marketeers-decisions-db`, private | `DATABASE_URL` |
| Study files | Private S3 bucket `marketeers-decisions-data`, read through the EC2 instance role | `MARKET_SIM_STORAGE=s3` |

The browser loads the UI from CloudFront and calls the API hostname directly. CloudFront does not proxy `/api`.

## How the two halves find each other

- **UI → API:** `npm run build` reads `frontend/.env.production`, which sets `VITE_API_BASE_URL=https://api-decisions.marketeers-ba.com`. `client.ts` prefixes every `/api/...` path with it. The value is compiled into the bundle, so changing it means rebuilding and re-uploading.
- **API → UI:** the API only answers browsers whose origin is listed in `MARKET_SIM_CORS_ORIGINS`. Production sets it to `https://decisions.marketeers-ba.com` in the server `.env`. Do not edit `main.py` to add origins.

## Server `.env` (EC2, `backend/.env`, mode 600)

```dotenv
APP_ENV=production
MARKET_SIM_CORS_ORIGINS=https://decisions.marketeers-ba.com

DATABASE_URL=postgresql+psycopg://postgres:<PASSWORD>@<rds-endpoint>:5432/marketeers_decisions

MARKET_SIM_STORAGE=s3
MARKET_SIM_S3_BUCKET=marketeers-decisions-data
AWS_REGION=eu-west-3

MARKET_SIM_AUTH=cognito
COGNITO_REGION=<pool region>
COGNITO_USER_POOL_ID=<pool id>
COGNITO_APP_CLIENT_ID=<app client id>
```

No AWS access keys: S3 credentials come from the instance role `MarketeersDecisionsS3DataAccess`. Study objects are written under the `clients/` prefix (`MARKET_SIM_S3_PREFIX`).

`MARKET_SIM_AUTH=dev` makes every caller the same `local-dev` client. Do not leave it on an internet-facing API with real data.

## Release the API

On the EC2 instance, from `~/Marketeers-Decisions/backend`. Deploy only committed code: no edits on the server.

```bash
git fetch && git checkout <release commit or tag>
source .venv/bin/activate
python -m pip install -r requirements.txt
alembic upgrade head
sudo systemctl restart marketeers-decisions
sudo journalctl -u marketeers-decisions -n 50 --no-pager
curl -sS http://127.0.0.1:8000/api/health
```

Startup also runs `alembic upgrade head`; running it first surfaces a migration error before the restart.

Keep one worker. The systemd `ExecStart` is `uvicorn api.main:app --host 0.0.0.0 --port 8000` with no `--workers`. Sessions and in-flight optimizer jobs live in that process, so a second worker or a second EC2 target behind the ALB splits them.

The target group health check should use `/api/health`, which returns `{"status":"ok"}` without rendering Swagger.

## Release the UI

On a workstation, from `frontend/`:

```powershell
npm ci
npm run build
```

Check that `dist/assets/*.js` contains `api-decisions.marketeers-ba.com`. Then, in the `marketeers-decisions-frontend` bucket:

1. Upload `dist/assets/` to `assets/` first.
2. Upload `dist/index.html` (and any other top-level files) to the bucket root.
3. Keep the previous assets until the release is accepted. Do not empty the bucket first.
4. Invalidate CloudFront distribution `E14K89NZ3KAV61` with `/*` and wait for Completed.

The distribution maps 403 and 404 to `/index.html` with status 200, so direct links such as `/data-hub` load the SPA.

## Single-process mode (local only)

The API can also serve the UI itself, which needs no CORS or API URL:

```powershell
powershell -ExecutionPolicy Bypass -File backend/scripts/build_spa.ps1
cd backend
uvicorn api.main:app --port 8000
```

```bash
bash backend/scripts/build_spa.sh
cd backend && uvicorn api.main:app --port 8000
```

The scripts build with `--mode same-origin`, which skips `.env.production`, then copy `frontend/dist` to `backend/api/spa_dist`. `api/main.py` mounts that directory at `/` when it exists. Without it, `/` is JSON and `/api/*` and `/docs` still work.

Production does not use this. Leave `backend/api/spa_dist` absent on the EC2 instance, and never commit it.

## Not in this repository

| Item | Where it lives |
|------|----------------|
| systemd unit | `/etc/systemd/system/marketeers-decisions.service` on EC2 ([backend record](../../deployment/Marketeers_Decisions_Backend_Architecture_and_Process.md)) |
| Server `.env` | `backend/.env` on EC2 |
| ALB, target group, ACM, Route 53, CloudFront, buckets, RDS, IAM role | AWS console; see [`docs/deployment/`](../../deployment/README.md) |
| CI/CD | None. Both releases above are manual. |
| Dockerfile | None. |
