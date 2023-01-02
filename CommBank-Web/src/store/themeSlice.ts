import { createSlice } from '@reduxjs/toolkit'
import { RootState } from './store'

export interface ThemeState {
  mode: 'light' | 'dark'
}

const initialState: ThemeState = {
  mode: 'dark',
}

export const themeSlice = createSlice({
  name: 'theme',
  initialState,
  reducers: {
    setLightMode: (state) => {
      state.mode = 'light'
    },
    setDarkMode: (state) => {
      state.mode = 'dark'
    },
  },
})

export const { setLightMode, setDarkMode } = themeSlice.actions

export const selectMode = (state: RootState) => state.theme.mode

export default themeSlice.reducer
