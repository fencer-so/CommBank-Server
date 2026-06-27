import { faChartLine, faGear, faRocket } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import React from 'react'
import styled from 'styled-components'
import CommBank from '../../../assets/images/commbank.svg'
import { media } from '../../utils/media'

export default function Navbar() {
  return (
    <DrawerContainer>
      <Section>
        <LogoWrapper>
          <Logo src={CommBank} />
        </LogoWrapper>

        <DrawerItem isSelected={true}>
          <FontAwesomeIcon icon={faRocket} size="2x" />
          <span>Dashboard</span>
        </DrawerItem>

        <DrawerItem isSelected={false}>
          <FontAwesomeIcon icon={faChartLine} size="2x" />
          <span>Goals</span>
        </DrawerItem>
      </Section>

      <Section>
        <DrawerItem isSelected={false}>
          <FontAwesomeIcon icon={faGear} size="2x" />
          <span>Settings</span>
        </DrawerItem>
      </Section>
    </DrawerContainer>
  )
}

const DrawerContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  overflow: hidden;

  padding: 1.5rem 1.5rem;
  width: 250px;
  height: 100%;
  background-color: rgb(var(--primary));
  z-index: 2;

  box-shadow: ${({ theme }) => theme.boxShadow};

  ${media('<tablet')} {
    display: none;
  }
`

const Section = styled.div`
  display: flex;
  flex-direction: column;
`

type DrawerItemProps = { isSelected: boolean }
const DrawerItem = styled.div<DrawerItemProps>`
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  padding: 1rem;
  width: 100%;
  border-radius: 12px;
  margin-top: 4rem;
  background-color: ${(props) => (props.isSelected ? 'rgba(254, 223, 3, 0.4)' : '')};
  color: ${(props) => (props.isSelected ? '' : 'rgba(174, 174, 174, 1)')};
  font-weight: ${(props) => (props.isSelected ? 'bold' : 'light')};
  span {
    font-size: 2rem;
    margin-left: 1rem;
  }

  svg {
    color: ${(props) => (props.isSelected ? '' : 'rgba(174, 174, 174, 1)')};
  }
`

const LogoWrapper = styled.a`
  display: flex;
  margin-right: auto;
  text-decoration: none;
  flex-direction: row;
  justify-content: center;
  width: 100%;

  margin-bottom: 10rem;
`

const Logo = styled.img`
  width: 100px;
  height: 100px;
  ${media('<tablet')} {
  }
`
