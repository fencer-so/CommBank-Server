# Background

## In Task 1 you implemented this endpoint:

```cs
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Goal updatedGoal)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        updatedGoal.Id = goal.Id;

        await _goalsService.UpdateAsync(id, updatedGoal);

        return NoContent();
    }
```

## In Task 2 you:

- [x] Implemented emoji picker
- [x] Updated local state
- [x] Updated Redux

## But the icon change does not persist on refresh!

- In this task you will sync frontend with backend by making a PUT request to the endpoint you set up in Task 1

- You will need to
  - [ ] Add a PUT request to `/api/Goal` in `lib.ts`
  - [ ] Update the `onClick` event handler in `GoalManager.tsx`

# Add PUT Request to Update Goal

```ts
// lib.ts

export async function updateGoal(goalId: string, updatedGoal: Goal): Promise<boolean> {
  try {
    await axios.put(`${API_ROOT}/api/Goal/${goalId}`, updatedGoal)
    return true
  } catch (error: any) {
    return false
  }
}
```

# Update On Click Event Handler

```ts
// GoalManager.tsx

import { updateGoal as updateGoalApi } from '../../../api/lib'

// ...
export function GoalManager(props: Props) {
  // ...
  const pickEmojiOnClick = () => (emoji: BaseEmoji, event: MouseEvent) => {
    const updatedGoal: Goal = {
      ...props.goal,
      icon: emoji.native ?? props.goal.icon,
      name: name ?? props.goal.name,
      targetDate: targetDate ?? props.goal.targetDate,
      targetAmount: targetAmount ?? props.goal.targetAmount,
    }

    updateGoalApi(props.goal.id, updatedGoal)
  }
}
```
