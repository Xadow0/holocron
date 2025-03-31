import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit'
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

export const fetchInhabitants = createAsyncThunk(
  'inhabitants/fetchInhabitants',
  async (_, { rejectWithValue }) => {  
    try {
      const response = await fetch('http://localhost:5149/api/inhabitants') //API URL
      if (!response.ok) throw new Error('Error al obtener habitantes')
      return await response.json() as Inhabitant[]
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

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
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchInhabitants.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchInhabitants.fulfilled, (state, action) => {
        state.loading = false
        state.list = action.payload
      })
      .addCase(fetchInhabitants.rejected, (state, action) => {
        state.loading = false
        state.error = action.payload as string
      })
  }
})

export const {
  setInhabitants,
  setLoading,
  setError,
  setPage
} = inhabitantsSlice.actions

export default inhabitantsSlice.reducer