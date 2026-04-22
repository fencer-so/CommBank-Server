const express = require("express");
const mongoose = require("mongoose");
require("dotenv").config();

const app = express();
app.use(express.json());

mongoose.connect(process.env.MONGO_URI)
  .then(() => console.log("MongoDB Connected"))
  .catch(err => console.log(err));

app.get("/", (req, res) => {
  res.send("Server is running");
});

const goalRoutes = require("./routes/goalRoutes");
app.use("/goals", goalRoutes);

app.listen(5000, () => {
  console.log("Server running on port 5000");
});