import React, { useEffect, useState } from 'react'
import styled from 'styled-components'
import { getTransactions as getTransactionsApi } from '../../../../api/lib'
import { Transaction } from '../../../../api/types'
import { Card } from '../../../components/Card'
import { SectionHeading } from '../../../components/SectionHeading'
import { TransparentButton } from '../../../components/TransparentButton'
import { media } from '../../../utils/media'
import TransactionsContent from './TransactionsContent'

export default function TransactionsSection() {
  const [transactions, setTransactions] = useState<Transaction[] | null>(null)

  useEffect(() => {
    async function fetch() {
      const response = await getTransactionsApi()

      if (response !== null) {
        setTransactions(response)
      }
    }

    fetch()
  }, [])

  return (
    <Container>
      <TopGroup>
        <SectionHeading>Recent Transactions</SectionHeading>

        <TransparentButton>
          <h4 className="alert">See All</h4>
        </TransparentButton>
      </TopGroup>

      <TransactionsContent transactions={transactions} />
    </Container>
  )
}

const Container = styled(Card)`
  display: flex;
  flex-direction: column;
  width: 400px;
  min-height: 400px;
  height: 80%;
  padding: 4rem 2rem;
  overflow-y: hidden;
  border-radius: 2rem;
  margin-top: 2rem;
  margin-bottom: 2rem;

  ${media('<desktop')} {
    height: 450px;
  }

  ${media('<tablet')} {
    width: 100%;
    min-height: 300px;
  }
`

const TopGroup = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  justify-content: space-between;

  h4.alert {
    font-size: 1.4rem;
    font-weight: bold;
  }

  ${media('<tablet')} {
    flex-direction: column;
  }
`
