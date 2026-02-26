import 'date-fns'
import React from 'react'
import styled from 'styled-components'
import { TransparentButton } from '../../components/TransparentButton'

type Props = { icon: string | null; onClick: (e: React.MouseEvent) => void }

export default function GoalIcon(props: Props) {
  return (
    <TransparentButton onClick={props.onClick}>
      <Icon>{props.icon}</Icon>
    </TransparentButton>
  )
}

const Icon = styled.h1`
  font-size: 6rem;
  cursor: pointer;
`
