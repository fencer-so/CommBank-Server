import styled from 'styled-components'
import { Theme } from './Theme'

export const Card = styled.div`
  background-color: ${({ theme }: { theme: Theme }) => theme.cardBackground};
  box-shadow: ${({ theme }: { theme: Theme }) => theme.boxShadow};
`
