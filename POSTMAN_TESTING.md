# Testing the CommBank API with Postman

## 1. Start the server

From the repo root:

```bash
cd CommBank-Server
dotnet run
```

Or run from Visual Studio / Rider. The API will listen on **http://localhost:5203** (or http://localhost:11366 — check the console for the port).

Use this as your **Base URL** in Postman: `http://localhost:5203` (replace with your actual port if different).

---

## 2. Set up Postman

- **Headers:** For POST/PUT, set `Content-Type: application/json` (Postman usually does this when you pick "raw" + "JSON").
- **Body:** For POST/PUT, choose **Body** → **raw** → **JSON**.

---

## 3. Test the main endpoints

### Health check (optional)

- **GET** `http://localhost:5203/swagger/index.html`  
  Opens Swagger UI in the browser so you can see all endpoints.

---

### Accounts

| Method | URL | Body (for POST/PUT) |
|--------|-----|----------------------|
| GET all | `http://localhost:5203/api/Account` | — |
| GET one | `http://localhost:5203/api/Account/{id}` | — |
| POST | `http://localhost:5203/api/Account` | See below |
| PUT | `http://localhost:5203/api/Account/{id}` | Same shape as POST |
| DELETE | `http://localhost:5203/api/Account/{id}` | — |

**Example POST body (create account):**
```json
{
  "name": "My Savings",
  "number": 12345678,
  "balance": 0,
  "accountType": "goalSaver"
}
```
Use `"accountType": "goalSaver"` or `"netBankSaver"`. For GET one / PUT / DELETE, `{id}` must be a 24-character MongoDB ObjectId (e.g. from a previous GET or POST response).

---

### Users

| Method | URL | Body |
|--------|-----|------|
| GET all | `http://localhost:5203/api/User` | — |
| GET one | `http://localhost:5203/api/User/{id}` | — |
| POST | `http://localhost:5203/api/User` | See below |
| PUT | `http://localhost:5203/api/User/{id}` | Same shape |
| DELETE | `http://localhost:5203/api/User/{id}` | — |

**Example POST body (create user — password is hashed by the server):**
```json
{
  "name": "Test User",
  "email": "test@example.com",
  "password": "secret123",
  "accountIds": [],
  "goalIds": [],
  "transactionIds": []
}
```

---

### Auth (login)

- **POST** `http://localhost:5203/api/Auth/Login`  

**Body:**
```json
{
  "email": "test@example.com",
  "password": "secret123"
}
```
- Success: **204 No Content**.  
- Wrong email/password: **404 Not Found**.

---

### Goals

| Method | URL | Body |
|--------|-----|------|
| GET all | `http://localhost:5203/api/Goal` | — |
| GET one | `http://localhost:5203/api/Goal/{id}` | — |
| GET by user | `http://localhost:5203/api/Goal/User/{userId}` | — |
| POST | `http://localhost:5203/api/Goal` | See below |
| PUT | `http://localhost:5203/api/Goal/{id}` | Same shape |
| DELETE | `http://localhost:5203/api/Goal/{id}` | — |

**Example POST body:**
```json
{
  "name": "Holiday Fund",
  "icon": "plane",
  "targetAmount": 5000,
  "targetDate": "2026-12-31T00:00:00Z",
  "balance": 0,
  "userId": "PUT_A_USER_OBJECT_ID_HERE"
}
```
`icon` is optional. Get a real `userId` from GET `/api/User`.

---

### Transactions

| Method | URL | Body |
|--------|-----|------|
| GET all | `http://localhost:5203/api/Transaction` | — |
| GET one | `http://localhost:5203/api/Transaction/{id}` | — |
| GET by user | `http://localhost:5203/api/Transaction/User/{userId}` | — |
| POST | `http://localhost:5203/api/Transaction` | See below |
| PUT | `http://localhost:5203/api/Transaction/{id}` | Same shape |
| DELETE | `http://localhost:5203/api/Transaction/{id}` | — |

**Example POST body:**
```json
{
  "transactionType": "credit",
  "amount": 100.50,
  "dateTime": "2026-03-05T12:00:00Z",
  "description": "Salary",
  "userId": "PUT_A_USER_OBJECT_ID_HERE"
}
```
Use `transactionType`: `"credit"`, `"debit"`, or `"transfer"`. `goalId` and `tagIds` are optional.

---

### Tags

| Method | URL | Body |
|--------|-----|------|
| GET all | `http://localhost:5203/api/Tag` | — |
| GET one | `http://localhost:5203/api/Tag/{id}` | — |
| POST | `http://localhost:5203/api/Tag` | `{ "name": "Groceries" }` |
| PUT | `http://localhost:5203/api/Tag/{id}` | `{ "name": "Food" }` |
| DELETE | `http://localhost:5203/api/Tag/{id}` | — |

---

## 4. Suggested test order

1. **GET** `/api/Account` and `/api/User` — see if the server and DB connection work (empty arrays `[]` is OK).
2. **POST** `/api/User` — create a user; copy the returned `id`.
3. **POST** `/api/Auth/Login` — login with that user’s email/password (expect 204).
4. **POST** `/api/Account` — create an account (use `"accountType": "goalSaver"` or `"netBankSaver"`).
5. **POST** `/api/Goal` — create a goal with the user’s `userId`; optionally include `"icon": "plane"`.
6. **GET** `/api/Goal` or `/api/Goal/User/{userId}` — confirm the goal (and optional `icon`) is returned.
7. **POST** `/api/Transaction` and **POST** `/api/Tag` — then GET to verify.

---

## 5. What to expect

- **GET (list):** `200` with a JSON array (possibly empty `[]`).
- **GET (one):** `200` with one object, or `404` if id not found.
- **POST:** `201 Created` with the created resource (including generated `id`) in the response.
- **PUT:** `204 No Content` on success, `404` if id not found.
- **DELETE:** `204 No Content` on success, `404` if id not found.
- **Login:** `204` on success, `404` if email/password wrong.

If you get **400**, check the request body (valid JSON, correct field names like `accountType`, `transactionType`, and allowed enum values). If you get **500**, check the server console and that MongoDB is reachable (e.g. connection string and Atlas network access).
