# Configuration

Tracks: `backend/config.py`, `backend/.env.example`, `frontend/.env.example`, `frontend/.env.production`.

The API reads these variables from `backend/.env`. In production that file lives on the EC2 instance and systemd passes it to `uvicorn`. The full production `.env` is in [Deployment](../operations/deployment.md#server-env-ec2-backendenv-mode-600).

## Local defaults

| Variable | Default | Role |
|----------|---------|------|
| `APP_ENV` | `development` | Adds `http://localhost:5173` and `http://127.0.0.1:5173` to CORS |
| `MARKET_SIM_STORAGE` | `local` | Study files on disk |
| `MARKET_SIM_AUTH` | `dev` | Every request is client `local-dev`. No login. |
| `MARKET_SIM_DEV_CLIENT_ID` | `local-dev` | That client id |
| `DATABASE_URL` | unset | SQLite at `backend/data/app.db` |
| `MARKET_SIM_DECIMALS` | `2` | Display rounding |
| `MARKET_SIM_ENFORCE_CALIBRATED_BOUNDS` | `true` | Runner rejects conditional edits outside calibrated lo/hi |
| `MARKET_SIM_USE_RESPONDENT_WEIGHT` | `false` | Every respondent has weight 1 |

`MARKET_SIM_STUDIES_DIR` overrides the study root (default `backend/studies`).

Schema changes are Alembic (`backend/alembic/`). Startup runs `alembic upgrade head`, or stamps head if the database predates Alembic.

## Mock login

Set `MARKET_SIM_AUTH=mock` and keep `APP_ENV=development`, then restart the API. Pages: `/signup`, `/confirm`, `/login`. The confirmation code is `000000`. Accounts are files under `backend/data/mock_auth/` (gitignored). Each email is its own tenant, so studies under the dev layout do not appear. Mock auth stays off when `APP_ENV` is anything else.

## Production settings

| Variable | Production value | Role |
|----------|------------------|------|
| `APP_ENV` | `production` | Localhost is not added to CORS. Mock auth is off. |
| `MARKET_SIM_CORS_ORIGINS` | `https://decisions.marketeers-ba.com` | Comma-separated browser origins allowed to call the API. Required because the UI is on CloudFront and the API on its own hostname. No `*`. |
| `DATABASE_URL` | `postgresql+psycopg://...` (RDS) | Catalog database |
| `MARKET_SIM_STORAGE` | `s3` | Study objects in S3. Requires `MARKET_SIM_S3_BUCKET`. |
| `MARKET_SIM_S3_BUCKET` | `marketeers-decisions-data` | Application-data bucket (not the UI bucket) |
| `MARKET_SIM_S3_PREFIX` | `clients` (default) | Key prefix for study objects |
| `AWS_REGION` | `eu-west-3` | Bucket region. The code default is `eu-west-1`, so set it. |
| `MARKET_SIM_AUTH` | `cognito` | Verify Cognito ID tokens. Map `sub` to a `clients` row. |
| `COGNITO_REGION`, `COGNITO_USER_POOL_ID`, `COGNITO_APP_CLIENT_ID` | pool values | Public app client, no secret |

AWS credentials come from the EC2 instance role. Do not put access keys in `.env`.

Run one `uvicorn` worker. Sessions and in-flight optimizer jobs live in that process.

Signup collects email and password. The confirm page sends the code to Cognito.

## Frontend

| Variable | Read by | Role |
|----------|---------|------|
| `VITE_API_PORT` | Vite dev server only | Port of the `/api` proxy target. Default `8000`. |
| `VITE_API_BASE_URL` | `npm run build` | API origin compiled into the bundle. `frontend/.env.production` sets `https://api-decisions.marketeers-ba.com`. No trailing slash, no `/api` suffix. Unset means same-origin (dev proxy or single-process mode). |

`VITE_*` values are public: they end up in the JavaScript the browser downloads. `backend/scripts/build_spa.*` builds with `--mode same-origin`, so `.env.production` does not apply to the single-process build.
