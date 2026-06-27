# Modify The Goal Manager So Users Can Add And Change Icons

- [ ] Add an `Add icon` component that:
  - [ ] Is only visible when there is no icon
  - [ ] Uses `TransparentButton` component
  - [ ] Opens the emoji picker component

```ts
// GoalManager.tsx

export function GoalManager(props: Props) {
  // ...

  const [icon, setIcon] = useState<string | null>(null)

  useEffect(() => {
    setIcon(props.goal.icon)
  }, [props.goal.id, props.goal.icon])

  const hasIcon = () => icon != null

  const addIconOnClick = (event: React.MouseEvent) => {
    event.stopPropagation()
    setEmojiPickerIsOpen(true)
  }

  return (
    {/* ... */}
    <AddIconButtonContainer hasIcon={hasIcon()}>
      <TransparentButton onClick={addIconOnClick}>
        <FontAwesomeIcon icon={faSmile} size="2x" />
        <AddIconButtonText>Add icon</AddIconButtonText>
      </TransparentButton>
    </AddIconButtonContainer>
    {/* ... */}
  )
}
```

- [ ] Add an `Icon` component that:
  - [ ] Is only visible when there is an icon
  - [ ] Uses `TransparentButton` component
  - [ ] Displays the icon with `font-size: 5.5rem;`
  - [ ] Opens the emoji picker component

```ts
// GoalIcon.tsx

const Icon = styled.h1`
  font-size: 6rem;
  cursor: pointer;
`

export default function GoalIcon(props: Props) {
  return (
    <TransparentButton onClick={props.onClick}>
      <Icon>{props.icon}</Icon>
    </TransparentButton>
  )
}

// GoalManager.tsx

type GoalIconContainerProps = { shouldShow: boolean }

const GoalIconContainer = styled.div<GoalIconContainerProps>`
  display: ${(props) => (props.shouldShow ? 'flex' : 'none')};
`

export function GoalManager(props: Props) {
  // ...

  const hasIcon = () => icon != null

  const goal = useAppSelector(selectGoalsMap)[props.goal.id]

  return (
    {/* ... */}
    <GoalIconContainer shouldShow={hasIcon()}>
      <GoalIcon icon={goal.icon} onClick={addIconOnClick} />
    </GoalIconContainer>
    {/* ... */}
  )
}
```
