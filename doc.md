# CommBank Server — Project Documentation

## Overview

CommBank Server is a RESTful API backend built with **ASP.NET Core (.NET 8)**, designed to power a personal banking and financial management application. It provides endpoints for managing users, bank accounts, financial goals, transactions, and tags — all backed by a **MongoDB Atlas** database.

The server is designed to be consumed by a frontend client (web or mobile), exposing a clean JSON API with full CRUD capabilities across all core resources.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| Language | C# |
| Database | MongoDB (via MongoDB Atlas) |
| ODM Driver | MongoDB.Driver 2.29.0 |
| Serialization | Newtonsoft.Json 13.0.1 |
| Password Hashing | BCrypt.Net-Next 4.0.3 |
| API Documentation | Swagger / Swashbuckle 6.2.3 |
| Dependency Injection | Microsoft.Extensions.DependencyInjection |

---

## Project Structure

```
CommBank-Server/
├── Controllers/
│   ├── AccountController.cs        # Bank account CRUD
│   ├── AuthController.cs           # User authentication (login)
│   ├── GoalController.cs           # Financial goals CRUD
│   ├── HomeController.cs           # Default/home route
│   ├── TagController.cs            # Tag management CRUD
│   ├── TransactionController.cs    # Transaction CRUD
│   └── UserController.cs           # User management CRUD
├── Models/
│   ├── Account.cs                  # Account entity
│   ├── AccountType.cs              # Enum: account types
│   ├── Goal.cs                     # Goal entity
│   ├── LoginInput.cs               # Auth input DTO
│   ├── ObjectIdConverter.cs        # MongoDB ObjectId JSON converter
│   ├── Tag.cs                      # Tag entity
│   ├── Transaction.cs              # Transaction entity
│   ├── TransactionType.cs          # Enum: transaction types
│   ├── UpdatedIcon.cs              # DTO for icon updates
│   └── User.cs                     # User entity
├── Services/                       # Business logic & database access layer
├── Program.cs                      # Application entry point and DI configuration
├── appsettings.json                # App configuration (MongoDB connection)
├── appsettings.Development.json    # Development overrides
└── CommBank.csproj                 # Project file and dependencies
```

---

## Domain Models

### User
Represents an application user.

| Field | Type | Description |
|---|---|---|
| `Id` | `string` (ObjectId) | Unique identifier |
| `Name` | `string` | Full name |
| `Email` | `string` | Email address |
| `Password` | `string` | Hashed password (BCrypt) |
| `AccountIds` | `List<string>` | References to linked accounts |
| `GoalIds` | `List<string>` | References to financial goals |
| `TransactionIds` | `List<string>` | References to transactions |

### Account
Represents a bank account belonging to a user.

| Field | Type | Description |
|---|---|---|
| `Id` | `string` (ObjectId) | Unique identifier |
| `Number` | `long` | Account number |
| `Name` | `string` | Account label/name |
| `Balance` | `double` | Current balance |
| `AccountType` | `AccountType` (enum) | Type of account |
| `TransactionIds` | `List<string>` | Linked transactions |

### Goal
Represents a savings or financial goal.

| Field | Type | Description |
|---|---|---|
| `Id` | `string` (ObjectId) | Unique identifier |
| `Name` | `string` | Goal name |
| `Icon` | `string` | Display icon identifier |
| `TargetAmount` | `UInt64` | Target amount to reach |
| `TargetDate` | `DateTime` | Deadline for the goal |
| `Balance` | `double` | Amount saved so far |
| `Created` | `DateTime` | Creation timestamp |
| `TransactionIds` | `List<string>` | Linked transactions |
| `TagIds` | `List<string>` | Associated tags |
| `UserId` | `string` | Owner user reference |

### Transaction
Represents a financial transaction.

| Field | Type | Description |
|---|---|---|
| `Id` | `string` (ObjectId) | Unique identifier |
| `TransactionType` | `TransactionType` (enum) | Type of transaction |
| `Amount` | `double` | Transaction amount |
| `DateTime` | `DateTime` | Timestamp |
| `GoalId` | `string` | Linked goal (if any) |
| `UserId` | `string` | Owner user reference |
| `TagIds` | `string[]` | Associated tags |
| `Description` | `string` | Optional description |

### Tag
Represents a label used to categorize transactions and goals.

| Field | Type | Description |
|---|---|---|
| `Id` | `string` (ObjectId) | Unique identifier |
| `Name` | `string` | Tag label |

---

## API Endpoints

### Authentication

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/Auth/Login` | Authenticate a user with email and password |

### Users — `/api/User`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/User` | Retrieve all users |
| `GET` | `/api/User/{id}` | Retrieve a user by ID |
| `POST` | `/api/User` | Create a new user |
| `PUT` | `/api/User/{id}` | Update a user by ID |
| `DELETE` | `/api/User/{id}` | Delete a user by ID |

### Accounts — `/api/Account`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Account` | Retrieve all accounts |
| `GET` | `/api/Account/{id}` | Retrieve an account by ID |
| `POST` | `/api/Account` | Create a new account |
| `PUT` | `/api/Account/{id}` | Update an account by ID |
| `DELETE` | `/api/Account/{id}` | Delete an account by ID |

### Goals — `/api/Goal`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Goal` | Retrieve all goals |
| `GET` | `/api/Goal/{id}` | Retrieve a goal by ID |
| `GET` | `/api/Goal/User/{id}` | Retrieve all goals for a specific user |
| `POST` | `/api/Goal` | Create a new goal (auto-links to user) |
| `PUT` | `/api/Goal/{id}` | Update a goal by ID |
| `DELETE` | `/api/Goal/{id}` | Delete a goal by ID |

### Transactions — `/api/Transaction`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Transaction` | Retrieve all transactions |
| `GET` | `/api/Transaction/{id}` | Retrieve a transaction by ID |
| `GET` | `/api/Transaction/User/{id}` | Retrieve all transactions for a specific user |
| `POST` | `/api/Transaction` | Create a new transaction |
| `PUT` | `/api/Transaction/{id}` | Update a transaction by ID |
| `DELETE` | `/api/Transaction/{id}` | Delete a transaction by ID |

### Tags — `/api/Tag`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Tag` | Retrieve all tags |
| `GET` | `/api/Tag/{id}` | Retrieve a tag by ID |
| `POST` | `/api/Tag` | Create a new tag |
| `PUT` | `/api/Tag/{id}` | Update a tag by ID |
| `DELETE` | `/api/Tag/{id}` | Delete a tag by ID |

---

## Configuration

### Database

The application connects to **MongoDB Atlas**. The connection string is loaded from a `Secrets.json` file (not committed to version control) and referenced via the `CommBank` connection string key.

To configure locally, create a `Secrets.json` file at the project root:

```json
{
  "ConnectionStrings": {
    "CommBank": "your-mongodb-connection-string"
  }
}
```

> **Note:** Never commit `Secrets.json` to source control. Add it to `.gitignore`.

### CORS

CORS is configured to allow all origins, methods, and headers — suitable for development and open API consumption. Restrict this in production environments.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A MongoDB Atlas account or local MongoDB instance

### Running the Server

```bash
# Clone the repository
git clone <repository-url>
cd CommBank-Server/CommBank-Server

# Add your Secrets.json with the MongoDB connection string

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

The API will be available at `http://localhost:<port>`.

### Swagger UI

In development mode, interactive API documentation is available at:

```
http://localhost:<port>/swagger
```

---

## Architecture Notes

- **Service Layer:** All database interactions are abstracted behind service interfaces (`IAccountsService`, `IUsersService`, etc.), registered as singletons in the DI container.
- **Goal–User Linking:** When a new goal is created, the server automatically appends the goal's ID to the owning user's `GoalIds` list, maintaining referential integrity at the application level.
- **Password Security:** User passwords are hashed using BCrypt via the `BCrypt.Net-Next` library.
- **ID Format:** All entities use MongoDB `ObjectId` (24-character hex strings) as their primary identifiers.

---

## Notes & Known Considerations

- HTTPS redirection is currently disabled (`app.UseHttpsRedirection()` is commented out). Enable it in production.
- There is no JWT-based authentication or session management yet. The login endpoint validates credentials but does not return a token.
- CORS is fully open — tighten the policy before deploying to production.

---

*Last updated: July 2026*
