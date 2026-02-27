import { faPlusCircle } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import React, { useEffect } from 'react'
import styled from 'styled-components'
import { createGoal as createGoalApi, getGoals } from '../../../../api/lib'
import { createGoal as createGoalRedux, selectGoalsList } from '../../../../store/goalsSlice'
import { useAppDispatch, useAppSelector } from '../../../../store/hooks'
import {
  setContent as setContentRedux,
  setIsOpen as setIsOpenRedux,
  setType as setTypeRedux,
} from '../../../../store/modalSlice'
import { SectionHeading } from '../../../components/SectionHeading'
import { media } from '../../../utils/media'
import GoalsContent from './GoalsContent'

export default function GoalsSection() {
  const dispatch = useAppDispatch()
  const goalIds = useAppSelector(selectGoalsList)

  useEffect(() => {
    async function fetch() {
      const goals = await getGoals()
      goals?.forEach((goal) => dispatch(createGoalRedux(goal)))
    }
    fetch()
  }, [dispatch])

  const onClick = async () => {
    const goal = await createGoalApi()

    if (goal != null) {
      dispatch(createGoalRedux(goal))
      dispatch(setContentRedux(goal))
      dispatch(setTypeRedux('Goal'))
      dispatch(setIsOpenRedux(true))
    }
  }

  return (
    <Container>
      <TopGroup>
        <SectionHeading>Goals</SectionHeading>
        <Icon onClick={onClick}>
          <FontAwesomeIcon icon={faPlusCircle} size="2x" className="alert" />
        </Icon>
      </TopGroup>

      <GoalsContent ids={goalIds} />
    </Container>
  )
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
  width: 400px;
  margin-top: 2rem;
  margin-bottom: 2rem;

  ${media('<tablet')} {
    width: 100%;
  }
`

const TopGroup = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;

  ${media('<tablet')} {
    flex-direction: column;
  }
`

const Icon = styled.a`
  margin-left: 1rem;
`
