const mongoose = require("mongoose");

const goalSchema = new mongoose.Schema({
  title: String,
  description: String,
  targetAmount: Number,
  currentAmount: Number,
  icon: {
    type: String,
    required: false
  }
});

module.exports = mongoose.model("Goal", goalSchema);