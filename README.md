# Commonwealth Bank Forage – Task 1

## Objective

The task was to extend the Goal model by adding a new property called **Icon** and ensure that the API returns this field when goal data is requested.

---

# Step 1: Understand the Existing Architecture

The project follows a typical ASP.NET Core + MongoDB architecture:

MongoDB Database
↓
Services Layer
↓
Controllers
↓
API Endpoints
↓
Postman / Swagger

When a request is sent to:

GET /api/Goal

the flow is:

GoalController
↓
GoalsService
↓
MongoDB Collection (Goals)
↓
Return JSON Response

---

# Step 2: Setup MongoDB Atlas

### Database

CommBank

### Collections

- accounts
- goals
- tags
- transactions
- users

Imported:

- Accounts.json
- Goals.json
- Tags.json
- Transactions.json
- Users.json

---

# Step 3: Connect Backend to MongoDB

In `Program.cs`:

```csharp
var mongoClient =
    new MongoClient(
        builder.Configuration.GetConnectionString("CommBank"));

var mongoDatabase =
    mongoClient.GetDatabase("CommBank");
```
This creates a connection to MongoDB Atlas and accesses the CommBank database.

---

# Step 4: Verify Existing API

Using Swagger and Postman:

```http
GET /api/Goal
```

Initially, goal objects did not contain an `icon` field.

---

# Step 5: Extend the Goal Model

File:

`Models/Goal.cs`

Added:

```csharp
[BsonElement("icon")]
public string? Icon { get; set; }
```

---

# Why This Was Needed

MongoDB documents contained an `icon` field, but the Goal model did not.

Without the property:

```csharp
public string? Icon { get; set; }
```

MongoDB could not map the field and threw:

```text
Element 'icon' does not match any field or property of class CommBank.Models.Goal
```

Adding the property fixed the issue.

---

# Step 6: Add Icon Values to MongoDB

Examples:

```json
{
  "Name": "House Down Payment",
  "icon": "home"
}
```

```json
{
  "Name": "Tesla Model Y",
  "icon": "car"
}
```

```json
{
  "Name": "Trip to London",
  "icon": "flight"
}
```

```json
{
  "Name": "Trip to NYC",
  "icon": "location"
}
```

---

# Step 7: Build and Run the Application

```bash
dotnet build
dotnet run
```

The backend started successfully.

---

# Step 8: Test the API Again

Request:

```http
GET /api/Goal
```

Response now includes:

```json
{
  "name": "House Down Payment",
  "balance": 73501.82,
  "icon": "home"
}
```

This confirms the implementation works correctly.

---

# Step 9: Commit and Push to GitHub

```bash
git add .
git commit -m "Completed CommBank Forage project"
git push origin main
```

Changes were successfully pushed to GitHub.

---

# Final Result

### Before

```json
{
  "name": "House Down Payment",
  "balance": 73501.82
}
```

### After

```json
{
  "name": "House Down Payment",
  "balance": 73501.82,
  "icon": "home"
}
```

---

# Skills Demonstrated

### MongoDB
- Atlas setup
- Database creation
- Collection creation
- JSON import
- Document updates

### ASP.NET Core
- Models
- Services
- Controllers
- Dependency Injection
- REST APIs

### API Testing
- Swagger
- Postman
- GET requests
- JSON responses

### Git & GitHub
- Commits
- Push operations
- Repository management

---

# Summary

Successfully extended the Goal model by adding an optional Icon property, updated MongoDB documents with icon values, and verified through Swagger and Postman that the API correctly returns the new icon field in goal responses.
