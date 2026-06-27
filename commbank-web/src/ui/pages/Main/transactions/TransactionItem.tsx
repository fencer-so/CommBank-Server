import axios from 'axios'
import React, { useEffect, useState } from 'react'
import styled from 'styled-components'
import { API_ROOT } from '../../../../api/lib'
import { Tag, Transaction } from '../../../../api/types'
import Chip from '../../../components/Chip'

type Props = { transaction: Transaction }

export function TransactionItem(props: Props) {
  const [tags, setTags] = useState<Tag[] | null>(null)

  useEffect(() => {
    async function fetch(tagId: string): Promise<Tag> {
      const response = await axios.get(`${API_ROOT}/api/Tag/${tagId}`)
      return response.data
    }

    async function fetchAll() {
      const tags: Tag[] = []
      for (const tagId of props.transaction.tagIds) {
        const tag = await fetch(tagId)
        tags.push(tag)
      }

      setTags(tags)
    }

    fetchAll()
  })

  return (
    <Container>
      <Content>
        <h6 className="description">{props.transaction.description}</h6>

        {tags ? tags.map((tag) => <Chip key={tag.id} label={tag.name} />) : null}

        <h6 className="datetime">{`${new Date(
          props.transaction.dateTime,
        ).toLocaleDateString()}`}</h6>

        <h6 className="price">{`${
          props.transaction.transactionType === 'Credit'
            ? `$${props.transaction.amount}`
            : `-$${props.transaction.amount}`
        }`}</h6>
      </Content>
      <Divider />
    </Container>
  )
}

const Container = styled.div`
  display: flex;
  flex-direction: column;
`

const Divider = styled.div`
  width: 100%;
  height: 0.2px;
  background-color: rgba(174, 174, 174, 0.6);
`

const Content = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;

  h6 {
    font-size: 1.2rem;
  }

  h6.datetime {
    color: rgba(174, 174, 174, 1);
    font-weight: bold;
  }
`
