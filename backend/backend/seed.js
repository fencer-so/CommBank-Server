const mongoose = require("mongoose");
const dotenv = require("dotenv");
const Goal = require("./models/Goal");

// ✅ ab simple path
const goalsData = require("./data/goals.json");

dotenv.config();

mongoose.connect(process.env.MONGO_URI)
  .then(async () => {
    console.log("MongoDB Connected for Seeding");

    await Goal.deleteMany();
    await Goal.insertMany(goalsData);

    console.log("Database Seeded ✅");
    process.exit();
  })
  .catch((err) => {
    console.error(err);
    process.exit(1);
  });