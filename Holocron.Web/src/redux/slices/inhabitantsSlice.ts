import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit'
import { Inhabitant } from '../../types/inhabitants'

const API_URL = 'http://localhost:5149/api/inhabitants'
interface InhabitantsState {
    list: Inhabitant[]
    searchResults: Inhabitant[]
    loading: boolean
    error: string | null
    page: number
    totalPages: number
}

export const fetchSearchResults = createAsyncThunk(
  'inhabitants/fetchSearchResults',
  async (query: string, { rejectWithValue }) => {
    try {
      const response = await fetch(`http://localhost:5149/api/inhabitants/search?query=${encodeURIComponent(query)}`)
      if (!response.ok) throw new Error('Error al buscar habitantes')
      return await response.json() as Inhabitant[]
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)


const initialState: InhabitantsState = {
  list: [],
  searchResults: [],
  loading: false,
  error: null,
  page: 1,
  totalPages: 1
}

export const createInhabitant = createAsyncThunk(
  'inhabitants/createInhabitant',
  async (inhabitantData: { name: string; species: string; origin?: string; isSuspectedRebel: boolean }, { rejectWithValue }) => {
    try {
      const response = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(inhabitantData),
      })
      if (!response.ok) throw new Error('Error al crear el habitante')
      return await response.json()
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const fetchRebels = createAsyncThunk(
  'inhabitants/fetchRebels',
  async (_, { rejectWithValue }) => {
    try {
      const response = await fetch('http://localhost:5149/api/inhabitants/rebels')  // URL de los rebeldes
      if (!response.ok) throw new Error('Error al obtener los rebeldes')
      return await response.json() as Inhabitant[]  // Asumimos que la respuesta ser� un array de habitantes
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const fetchInhabitants = createAsyncThunk(
  'inhabitants/fetchInhabitants',
  async (_, { rejectWithValue }) => {
    try {
      const response = await fetch(API_URL) //API URL
      if (!response.ok) throw new Error('Error al obtener habitantes')
      return await response.json() as Inhabitant[]
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const deleteInhabitant = createAsyncThunk(
  'inhabitants/deleteInhabitant',
  async (id: number, { rejectWithValue }) => {
    try {
      await fetch(`${API_URL}/${id}`, { method: 'DELETE' })
      return id
    } catch (error) {
      return rejectWithValue('Error eliminando el habitante')
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
      .addCase(deleteInhabitant.fulfilled, (state, action) => {
        state.list = state.list.filter(inhabitant => inhabitant.id !== action.payload)
      })
      .addCase(createInhabitant.fulfilled, (state, action) => {
        state.list.push(action.payload) 
      })
      .addCase(createInhabitant.rejected, (state, action) => {
        state.error = action.payload as string
      })
      .addCase(fetchSearchResults.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchSearchResults.fulfilled, (state, action) => {
        state.loading = false
        state.searchResults = action.payload
      })
      .addCase(fetchSearchResults.rejected, (state, action) => {
        state.loading = false
        state.error = action.payload as string
      })
      .addCase(fetchRebels.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchRebels.fulfilled, (state, action) => {
        state.loading = false
        state.searchResults = action.payload 
      })
      .addCase(fetchRebels.rejected, (state, action) => {
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
