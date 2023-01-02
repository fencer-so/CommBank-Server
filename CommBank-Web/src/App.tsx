import { createTheme, ThemeProvider as ThemeProviderMui } from '@material-ui/core'
import React, { useEffect } from 'react'
import styled, { ThemeProvider } from 'styled-components'
import { getUser as getUserApi } from './api/lib'
import { useAppDispatch, useAppSelector } from './store/hooks'
import { selectIsOpen, setIsOpen as setIsOpenRedux } from './store/modalSlice'
import { selectMode } from './store/themeSlice'
import { setUser as setUserRedux } from './store/userSlice'
import { GlobalStyle } from './ui/components/GlobalStyles'
import { DarkTheme, LightTheme } from './ui/components/Theme'
import Main from './ui/pages/Main/Main'
import Modal from './ui/surfaces/modal/Modal'

export default function App() {
  const mode = useAppSelector(selectMode)
  const modalIsOpen = useAppSelector(selectIsOpen)
  const dispatch = useAppDispatch()

  const muiTheme = createTheme({ palette: { type: mode } })

  useEffect(() => {
    async function fetch() {
      const user = await getUserApi()
      if (user != null) {
        dispatch(setUserRedux(user))
      }
    }

    fetch()
  }, [dispatch])

  const onClick = (event: React.MouseEvent) => {
    event.stopPropagation()
    dispatch(setIsOpenRedux(false))
  }

  return (
    <AppContainer onClick={onClick}>
      <ThemeProviderMui theme={muiTheme}>
        <ThemeProvider theme={mode === 'light' ? LightTheme : DarkTheme}>
          <GlobalStyle />

          <Main />

          <ModalContainer isOpen={modalIsOpen}>
            <Modal />
          </ModalContainer>
        </ThemeProvider>
      </ThemeProviderMui>
    </AppContainer>
  )
}

const AppContainer = styled.div`
  position: relative;
`
const ModalContainer = styled.div<ModalContainerProps>`
  width: 100vw;
  height: 100vh;
  display: ${(props) => (props.isOpen ? 'flex' : 'none')};
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background-color: ${({ theme }) => theme.overlay};
  position: absolute;
  top: 0;
  left: 0;
`

type ModalContainerProps = { isOpen: boolean }
