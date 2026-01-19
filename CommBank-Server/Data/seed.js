// Database seeding script for CommBank
// Run this script to populate the database with initial data

// Sample users
const users = [
    {
        _id: "user123",
        username: "john_doe",
        email: "john@example.com",
        password: "$2a$10$...", // hashed password
        created_at: new Date(),
        updated_at: new Date()
    },
    {
        _id: "user456", 
        username: "jane_smith",
        email: "jane@example.com",
        password: "$2a$10$...", // hashed password
        created_at: new Date(),
        updated_at: new Date()
    }
];

// Sample accounts
const accounts = [
    {
        _id: "acc123",
        user_id: "user123",
        account_type: "checking",
        balance: 1500.00,
        created_at: new Date(),
        updated_at: new Date()
    },
    {
        _id: "acc456",
        user_id: "user456", 
        account_type: "savings",
        balance: 5000.00,
        created_at: new Date(),
        updated_at: new Date()
    }
];

// Sample goals
const goals = [
    {
        _id: "goal123",
        user_id: "user123",
        account_id: "acc123",
        name: "Emergency Fund",
        target_amount: 10000.00,
        current_amount: 1500.00,
        deadline: new Date("2024-12-31"),
        icon: "🎯",
        created_at: new Date(),
        updated_at: new Date()
    },
    {
        _id: "goal456",
        user_id: "user456",
        account_id: "acc456", 
        name: "Vacation Fund",
        target_amount: 3000.00,
        current_amount: 500.00,
        deadline: new Date("2024-06-30"),
        icon: "✈️",
        created_at: new Date(),
        updated_at: new Date()
    }
];

// Sample transactions
const transactions = [
    {
        _id: "trans123",
        user_id: "user123",
        account_id: "acc123",
        amount: -200.00,
        transaction_type: "withdrawal",
        description: "ATM withdrawal",
        created_at: new Date(),
        updated_at: new Date()
    },
    {
        _id: "trans456",
        user_id: "user123", 
        account_id: "acc123",
        amount: 500.00,
        transaction_type: "deposit",
        description: "Salary deposit",
        created_at: new Date(),
        updated_at: new Date()
    }
];

// Sample tags
const tags = [
    {
        _id: "tag123",
        user_id: "user123",
        name: "Bills",
        color: "#FF6B6B",
        created_at: new Date(),
        updated_at: new Date()
    },
    {
        _id: "tag456",
        user_id: "user456",
        name: "Food",
        color: "#4CAF50",
        created_at: new Date(),
        updated_at: new Date()
    }
];

console.log("Database seeding data created successfully!");
console.log("Users:", users.length);
console.log("Accounts:", accounts.length); 
console.log("Goals:", goals.length);
console.log("Transactions:", transactions.length);
console.log("Tags:", tags.length);
