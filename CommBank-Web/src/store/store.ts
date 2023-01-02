import { Action, configureStore, ThunkAction } from '@reduxjs/toolkit'
import goalsReducer from './goalsSlice'
import modalReducer from './modalSlice'
import themeReducer from './themeSlice'
import userReducer from './userSlice'

export const store = configureStore({
  reducer: {
    goals: goalsReducer,
    modal: modalReducer,
    theme: themeReducer,
    user: userReducer,
  },
})

export type AppDispatch = typeof store.dispatch
export type RootState = ReturnType<typeof store.getState>
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>
