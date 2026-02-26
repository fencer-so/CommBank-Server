import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { ModalContent, ModalType } from '../api/types'
import { RootState } from './store'

export interface ModalState {
  isOpen: boolean
  type: ModalType | null
  content: ModalContent | null
}

const initialState: ModalState = {
  isOpen: false,
  type: null,
  content: null,
}

export const modalSlice = createSlice({
  name: 'modal',
  initialState,
  reducers: {
    setContent: (state, action: PayloadAction<ModalContent>) => {
      state.content = action.payload
    },
    setIsOpen: (state, action: PayloadAction<boolean>) => {
      state.isOpen = action.payload
    },
    setType: (state, action: PayloadAction<ModalType>) => {
      state.type = action.payload
    },
  },
})

export const { setContent, setIsOpen, setType } = modalSlice.actions

export const selectIsOpen = (state: RootState) => state.modal.isOpen
export const selectContent = (state: RootState) => state.modal.content
export const selectType = (state: RootState) => state.modal.type

export default modalSlice.reducer
