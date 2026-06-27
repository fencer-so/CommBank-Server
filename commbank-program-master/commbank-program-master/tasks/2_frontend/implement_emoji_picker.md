# Implement An Emoji Picker

- [ ] Install `emoji-mart` at [v3.0.1](https://github.com/missive/emoji-mart/tree/v3.0.1)

```shell
npm install emoji-mart@3.0.1
```

- [ ] Create a reusable wrapper that:
  - [ ] Uses `mode` from Redux `themeSlice`
  - [ ] Takes an `onClick` event handler with params: `(emoji: BaseEmoji, event: React.MouseEvent) => void`

```ts
import { BaseEmoji, Picker } from 'emoji-mart'
import 'emoji-mart/css/emoji-mart.css'
import { useAppSelector } from '../../store/hooks'
import { selectMode } from '../../store/themeSlice'

type Props = { onClick: (emoji: BaseEmoji, event: React.MouseEvent) => void }

export default function EmojiPicker(props: Props) {
  const theme = useAppSelector(selectMode)

  return (
    <Picker
      theme={theme}
      showPreview={false}
      showSkinTones={false}
      onClick={props.onClick}
      color="primary"
    />
  )
}
```

- [ ] Display the emoji picker based on `useState` hook

```ts
// GoalManager.tsx

type EmojiPickerContainerProps = { isOpen: boolean; hasIcon: boolean }

const EmojiPickerContainer = styled.div<EmojiPickerContainerProps>`
  display: ${(props) => (props.isOpen ? 'flex' : 'none')};
  position: absolute;
  top: ${(props) => (props.hasIcon ? '10rem' : '2rem')};
  left: 0;
`

export function GoalManager(props: Props) {
  const [emojiPickerIsOpen, setEmojiPickerIsOpen] = useState(false)

  const hasIcon = () => icon != null

  const pickEmojiOnClick = (emoji: BaseEmoji, event: MouseEvent) => {
    // TODO(TASK-2) Stop event propogation
    // TODO(TASK-2) Set icon locally
    // TODO(TASK-2) Close emoji picker
    // TODO(TASK-2) Create updated goal locally
    // TODO(TASK-2) Update Redux store
    // TODO(TASK-3) Update database
  }

  return (
    {/* ... */}

    <EmojiPickerContainer
      isOpen={emojiPickerIsOpen}
      hasIcon={hasIcon()}
      onClick={(event) => event.stopPropagation()}
    >
      <EmojiPicker onClick={pickEmojiOnClick} />
    </EmojiPickerContainer>

    {/* ... */}
  )
}
```

- [ ] Implement the `pickEmojiOnClick` event handler:
  - [ ] Stop event propogation
  - [ ] Set icon locally
  - [ ] Close emoji picker
  - [ ] Create updated goal locally
  - [ ] Update Redux store

```ts
const pickEmojiOnClick = (emoji: BaseEmoji, event: MouseEvent) => {
  event.stopPropagation()

  setIcon(emoji.native)
  setEmojiPickerIsOpen(false)

  const updatedGoal: Goal = {
    ...props.goal,
    icon: emoji.native ?? props.goal.icon,
    name: name ?? props.goal.name,
    targetDate: targetDate ?? props.goal.targetDate,
    targetAmount: targetAmount ?? props.goal.targetAmount,
  }

  dispatch(updateGoalRedux(updatedGoal))

  // TODO(TASK-3) Update database
}
```
