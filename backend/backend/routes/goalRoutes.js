const express = require("express");
const router = express.Router();
const Goal = require("../models/Goal");

// GET goals
router.get("/", async (req, res) => {
  const goals = await Goal.find();
  res.json(goals);
});

// POST goal
router.post("/", async (req, res) => {
  const goal = new Goal(req.body);
  await goal.save();
  res.json(goal);
});

module.exports = router;