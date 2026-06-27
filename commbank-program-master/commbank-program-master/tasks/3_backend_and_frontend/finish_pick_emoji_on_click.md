# Finish Implementing `pickEmojiOnClick` Event Handler

- [ ] Import `updateGoal` PUT request
- [ ] Call `updateGoal` on click

```ts
// GoalManager.tsx

import { updateGoal as updateGoalApi } from '../../../api/lib'
// ...

export function GoalManager(props: Props) {
  // ...

  const pickEmojiOnClick = () => (emoji: BaseEmoji, event: MouseEvent) => {
    // ...

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
