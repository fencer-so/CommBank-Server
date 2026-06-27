# Add PUT Request To Lib

- [ ] Add PUT request to update Goal

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
