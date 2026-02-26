# Goal Manager: Backend API, Frontend UI & Tests

## Summary
This PR adds a full **Goal Manager** feature: backend REST API for goals, a React frontend to create and manage savings goals with icons, and unit tests to verify the API behaviour.

## Changes

### Backend (CommBank-Server)
- **Goal model** (`Models/Goal.cs`): Extended with `Icon`, `TransactionIds`, and `TagIds` for richer goal data.
- **GoalController** (existing): REST API at `/api/Goal` — GET all, GET by id, POST, PUT, DELETE.
- **GoalService / IGoalsService** (existing): CRUD and MongoDB integration.
- **Config**: `Secrets.json.example` for local MongoDB connection template; `.gitignore` updated to exclude `Secrets.json`.

### Frontend (commbank-web)
- **React app** (TypeScript): New `commbank-web` app with Redux store, API client, and UI components.
- **Goal Manager**: Create/edit/delete goals with name, target amount, target date, balance, and **icon** (emoji picker).
- **Goals section**: Goal cards on the main page, wired to the goals API.
- **Shared UI**: Cards, modals, date picker, theme switcher, navbar, drawer.

### Tests
- **GoalControllerTests**: Unit tests for `Get` (all), `Get(id)`, `Post`, `Put`, and `Delete` using fake services.

### Data & setup
- **Seed data**: `data/*.json` (Goals, Accounts, Tags, Transactions, Users) for local development.
- **Scripts**: `scripts/seed.sh` for seeding the database.
- **Docs**: `docs/SETUP.md` for setup and run instructions.

## How to test
1. Backend: Run CommBank-Server, ensure MongoDB is configured (see `Secrets.json.example`).
2. Frontend: From `commbank-web/`, run `npm install` and `npm start`.
3. Tests: Run `CommBank.Tests` (e.g. `dotnet test`).

## Checklist
- [x] Backend API for goals (GET/POST/PUT/DELETE)
- [x] Goal model includes Icon, TransactionIds, TagIds
- [x] Frontend Goal Manager UI wired to API
- [x] Unit tests for GoalController
- [x] Setup docs and seed data
