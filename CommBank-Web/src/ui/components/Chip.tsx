import styled from 'styled-components'
import { BLUE, GREEN, ORANGE, PURPLE, YELLOW } from '../colors'

type ChipProps = { label: string }

export default function Chip(props: ChipProps) {
  return (
    <ChipContainer label={props.label}>
      <span>{props.label}</span>
    </ChipContainer>
  )
}

const ChipContainer = styled.div<ChipProps>`
  display: flex;
  background-color: ${(props) => tagToColor[props.label]};
  border-radius: 2rem;
  padding: 1rem;
  font-weight: bold;
`

interface TagToColor {
  [label: string]: string
}

const tagToColor: TagToColor = {
  Groceries: GREEN,
  Restaurant: YELLOW,
  Income: ORANGE,
  Gas: BLUE,
  Investment: PURPLE,
}
