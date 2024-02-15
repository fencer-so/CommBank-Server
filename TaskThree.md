```
export async function updateGoal(goalId: string, updatedGoal: Goal): Promise<boolean> {
  try {
    await axios.put(`${API_ROOT}/api/Goal/${goalId}`, updatedGoal)
    return true
  } catch (error: any) {
    return false
  }
}
```

```
{
    "updatedGoal": {
        "id": "62a3f62e102e921da1253d34",
        "name": "Trip to London",
        "targetAmount": 3500,
        "targetDate": "2022-08-02T04:00:00Z",
        "balance": 753.89,
        "created": "2022-06-11T01:55:58.236Z",
        "transactionIds": null,
        "tagIds": null,
        "icon": "🤺",
        "userId": "________"
    }
}
```