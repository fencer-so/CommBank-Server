import React from 'react'
import styled from 'styled-components'
import { media } from '../../../utils/media'
import GoalCard from './GoalCard'

type Props = { ids: string[] | null }

export default function GoalsContent(props: Props) {
  if (!props.ids) return null

  return (
    <Container>
      {props.ids.map((id) => (
        <GoalCard key={id} id={id} />
      ))}
    </Container>
  )
}

const Container = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  width: 400px;
  padding: 4rem;
  overflow-x: auto;

  ${media('<tablet')} {
    width: 100%;

    padding-left: 0;
    padding-right: 0;
  }
`
