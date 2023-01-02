import { faBell, faEnvelope } from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import React from 'react'
import styled from 'styled-components'
import Tag from '../../../assets/images/tag.png'
import { useAppSelector } from '../../../store/hooks'
import { selectUser } from '../../../store/userSlice'
import ThemeSwitcher from '../../features/themeswitcher/ThemeSwitcher'
import { media } from '../../utils/media'

export default function Navbar() {
  const user = useAppSelector(selectUser)

  return (
    <Container>
      <NavbarActions>
        <NavbarAction>
          <ThemeSwitcher />
        </NavbarAction>

        <NavbarAction>
          <FontAwesomeIcon icon={faEnvelope} size="2x" />
        </NavbarAction>

        <NavbarAction>
          <FontAwesomeIcon icon={faBell} size="2x" />
        </NavbarAction>

        <UserGroup>
          <NavbarAction>
            <Avatar src={Tag} />
          </NavbarAction>

          <UserNameAndEmailGroup>
            <span>{user?.name}</span>
            <span>{user?.email}</span>
          </UserNameAndEmailGroup>
        </UserGroup>
      </NavbarActions>
    </Container>
  )
}

const Container = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  position: sticky;
  top: 0;
  padding: 1.5rem 1.5rem;
  width: 100%;
  height: 8rem;
`

const NavbarActions = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
`

const NavbarAction = styled.a`
  margin: 0.8rem;
`

const Avatar = styled.img`
  width: 50px;
  height: 50px;
  border-radius: 50%;
`

const UserGroup = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
`

const UserNameAndEmailGroup = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: flex-start;

  ${media('<tablet')} {
    display: none;
  }
`
