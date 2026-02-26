import React from 'react'
import styled from 'styled-components'
import CommBankCard from '../../../../assets/images/commbank_card.svg'
import { Card } from '../../../components/Card'
import { SectionHeading } from '../../../components/SectionHeading'
import { media } from '../../../utils/media'

export default function AccountsSection() {
  return (
    <Container>
      <TopGroup>
        <SectionHeading>Accounts</SectionHeading>
      </TopGroup>
      <Img src={CommBankCard} />
    </Container>
  )
}

const Container = styled(Card)`
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 4rem 2rem;
  width: 400px;
  height: 300px;
  border-radius: 2rem;
  margin-top: 2rem;
  margin-bottom: 2rem;

  ${media('<tablet')} {
    width: 100%;
  }
`
const TopGroup = styled.div`
  display: flex;
  flex-direction: row;

  ${media('<tablet')} {
    flex-direction: column;
  }
`

const Img = styled.img`
  width: 350px;
  height: 209px;
`
