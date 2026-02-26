# Task 5: Create a Pull Request – Instructions

Your branch **feature/goal-manager** is created and all changes are **committed** locally. You need to push to **your fork** and then open a PR on GitHub.

---

## Step 1: Fork the repository (if you haven’t already)

1. Go to: **https://github.com/fencer-so/commbank-server**
2. Click **Fork** (top right) to create a fork under your account (e.g. `https://github.com/deepakgoudasirsi/commbank-server`).

---

## Step 2: Add your fork as a remote and push

In your project folder (`cc`), run (replace `YOUR_GITHUB_USERNAME` with your GitHub username, e.g. `deepakgoudasirsi`):

```bash
cd /Users/deepakgouda/cc

# Add your fork as remote (name: myfork)
git remote add myfork https://github.com/YOUR_GITHUB_USERNAME/commbank-server.git

# Push your branch to your fork
git push -u myfork feature/goal-manager
```

If you already added a fork with a different remote name, use that name instead of `myfork` (e.g. `git push -u origin feature/goal-manager` if origin was changed to your fork).

---

## Step 3: Open the Pull Request on GitHub

1. Go to **your fork** on GitHub: `https://github.com/YOUR_GITHUB_USERNAME/commbank-server`
2. You should see a banner: **“feature/goal-manager had recent pushes”** with a button **“Compare & pull request”**. Click it.
   - Or: switch branch to `feature/goal-manager`, then click **“Contribute”** → **“Open pull request”**.
3. Set:
   - **Base repository:** `fencer-so/commbank-server`, branch **main**
   - **Head repository:** `YOUR_GITHUB_USERNAME/commbank-server`, branch **feature/goal-manager**
4. **Title:** e.g. `Add Goal Manager: backend API, frontend UI, and tests`
5. **Description:** Copy the contents of **docs/PULL_REQUEST_DESCRIPTION.md** from this repo and paste into the PR description.
6. Click **“Create pull request”**.

---

## Step 4: Submit the task

1. Copy the URL of the new pull request (e.g. `https://github.com/fencer-so/commbank-server/pull/123`).
2. Put that link in the file **task5-submission.txt** (see below).
3. Upload **task5-submission.txt** where the task asks you to submit your file.

---

## Summary of what’s already done

- Branch **feature/goal-manager** created  
- All relevant changes committed (backend, frontend, tests, data, docs)  
- Push to **fencer-so/commbank-server** failed (no write access) — so you push to **your fork** and open a PR from there.
