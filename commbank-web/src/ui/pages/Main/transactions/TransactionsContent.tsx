import React from 'react'
import { Transaction } from '../../../../api/types'
import { TransactionItem } from './TransactionItem'

type Props = { transactions: Transaction[] | null }

export default function TransactionsContent(props: Props) {
  if (!props.transactions) return null
  return (
    <>
      {props.transactions.sort(sortByDateDesc).map((transaction) => (
        <TransactionItem transaction={transaction} />
      ))}
    </>
  )
}

function sortByDateDesc(a: Transaction, b: Transaction) {
  return new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime()
}
