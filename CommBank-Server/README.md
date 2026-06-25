🟢 PART 1 — Test DELETE (Swagger)
👉 Step 1: Open Swagger

Go to:

http://localhost:5203/swagger

Find:

DELETE /api/Goal/{id}

Click Try it out

👉 Step 2: Use your goal ID

Use this:

6a3ba1cb273255408b139419
👉 Step 3: Execute

Click Execute

🎯 Expected result
204 No Content

That means:
✔ Goal deleted successfully
✔ MongoDB updated
✔ API working fully

👉 Step 4: Confirm deletion

Run GET again:

GET /api/Goal

👉 Your "Updated Test Goal" should be gone

🟢 PART 2 — README.md (copy-paste ready)

Create a file in project root:

README.md

Paste this:

📄 CommBank Server – .NET + MongoDB Backend
🚀 Project Overview

This project is a backend REST API built using ASP.NET Core (.NET 6) and MongoDB Atlas.
It provides banking-style functionality including Goals, Users, Transactions, Tags, and Accounts.

⚙️ Tech Stack
ASP.NET Core Web API
C# (.NET 6)
MongoDB Atlas
MongoDB.Driver
Swagger (API Testing)
📦 Features
CRUD operations for Goals
User management system
MongoDB cloud database integration
RESTful API design
Swagger UI for testing endpoints
Goal model extended with optional Icon field
🧠 Database Setup

Database used: CommBank (MongoDB Atlas)

Collections:

Users
Goals
Tags
Transactions
Accounts
🔌 Connection Setup

Update Secrets.json:

{
"ConnectionStrings": {
"CommBank": "your-mongodb-connection-string"
}
}
▶️ How to Run Project
1. Restore dependencies
   dotnet restore
2. Build project
   dotnet build
3. Run project
   dotnet run
   🌐 API Access

Once running:

Swagger UI:
http://localhost:5203/swagger
Base URL:
http://localhost:5203/api
🧪 API Testing Performed

✔ GET /api/Goal
✔ POST /api/Goal
✔ PUT /api/Goal/{id}
✔ DELETE /api/Goal/{id}

✨ Model Update

Goal model extended with:

public string? Icon { get; set; }
👨‍💻 Author

Arif Khan

🟢 PART 3 — Git (FINAL SUBMISSION)

Run this in terminal:

cd C:\Users\arifx\CommBank-Server\CommBank-Server
👉 Step 1: Initialize (if needed)
git init
👉 Step 2: Add files
git add .
👉 Step 3: Commit
git commit -m "Task 1 completed: MongoDB integration, Goal CRUD, Icon field added, full API testing"
👉 Step 4: Connect repo
git remote add origin <your-github-repo-url>
👉 Step 5: Push
git push -u origin main
🎯 FINAL CHECKLIST (SUBMIT READY)

✔ DELETE tested
✔ Swagger tests done
✔ MongoDB working
✔ README added
✔ Git pushed
✔ Goal model updated