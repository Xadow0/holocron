import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { Inhabitant } from '../../types/inhabitants'

interface InhabitantsState {
    list: Inhabitant[]
    loading: boolean
    error: string | null
    page: number
    totalPages: number
}

const initialState: InhabitantsState = {
  list: [],
  loading: false,
  error: null,
  page: 1,
  totalPages: 1
}

const inhabitantsSlice = createSlice({
  name: 'inhabitants',
  initialState,
  reducers: {
    setInhabitants: (state, action: PayloadAction<Inhabitant[]>) => {
      state.list = action.payload
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload
    },
    setError: (state, action: PayloadAction<string | null>) => {
      state.error = action.payload
    },
    setPage: (state, action: PayloadAction<number>) => {
      state.page = action.payload
    }
  }
})

export const {
  setInhabitants,
  setLoading,
  setError,
  setPage
} = inhabitantsSlice.actions

export default inhabitantsSlice.reducer