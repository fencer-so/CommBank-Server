# Icon Support Implementation Plan

Current Working Directory: Backend. Frontend at ../commbank-web.

## Steps from Approved Plan:

### 1. Create TODO.md [COMPLETED]

### 2. Install frontend dependencies

- cd ../commbank-web && npm install (assumed done, package-lock present)
- Verify emoji-mart installed. [COMPLETED]

### 4. Update GoalCard.tsx to display icon [COMPLETED]

- Import/use GoalIcon component.

### 5. Update GoalManager.tsx (main) [COMPLETED]

- Add icon state, useEffect sync.
- Add Icon Group with GoalIcon/AddIconButton.
- Toggle EmojiPicker on click.
- Handle emoji select: update icon, dispatch/API.

### 6. Start dev servers

- Frontend: cd ../commbank-web && npm start
- Backend: dotnet run (if not running)

### 7. Test full flow

- Create goal.
- Open modal, add/change icon via picker.
- Verify display in card, API sync (backend Mongo?).

### 8. Commit to branch `blackboxai/icons`

- git checkout -b blackboxai/icons
- git add/commit/push

### 9. attempt_completion with GoalManager.tsx content.

**Next step: 2**
