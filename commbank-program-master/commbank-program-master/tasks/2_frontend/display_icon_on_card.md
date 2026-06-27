# Display The Icon On The Goal Card

```tsx
// GoalCard.tsx

const Icon = styled.h1`
  font-size: 5.5rem;
`

export default function GoalCard(props: Props) {
  // ...

  return (
    <Container key={goal.id} onClick={onClick}>
      {/* ... */}

      <Icon>{goal.icon}</Icon>
    </Container>
  )
}
```
