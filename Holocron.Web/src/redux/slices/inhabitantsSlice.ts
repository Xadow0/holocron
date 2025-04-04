import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit'
import { Inhabitant } from '../../types/inhabitants'

// Define una variable central para la API URL
const API_URL = process.env.VITE_BACKEND_BASE_URL

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
      // Utiliza API_URL con el endpoint específico para búsqueda
      const response = await fetch(`${API_URL}/search?query=${encodeURIComponent(query)}`)
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
      // Utiliza API_URL con el endpoint para rebeldes
      const response = await fetch(`${API_URL}/rebels`)
      if (!response.ok) throw new Error('Error al obtener los rebeldes')
      return await response.json() as Inhabitant[]
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const updateInhabitant = createAsyncThunk(
  'inhabitants/updateInhabitant',
  async ({ id, inhabitantData }: { id: string, inhabitantData: { name: string; species: string; origin: string; isSuspectedRebel: boolean } }, { rejectWithValue }) => {
    try {
      // Utiliza API_URL con el ID para la actualización
      const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(inhabitantData),
      })
      if (!response.ok) throw new Error('Error al actualizar el habitante')
      return await response.json()
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const fetchInhabitants = createAsyncThunk(
  'inhabitants/fetchInhabitants',
  async (_, { rejectWithValue }) => {
    try {
      // Utiliza API_URL para obtener todos los habitantes
      const response = await fetch(API_URL)
      if (!response.ok) throw new Error('Error al obtener habitantes')
      return await response.json() as Inhabitant[]
    } catch (error: any) {
      return rejectWithValue(error.message)
    }
  }
)

export const deleteInhabitant = createAsyncThunk(
  'inhabitants/deleteInhabitant',
  async (id: string, { rejectWithValue }) => {
    try {
      // Utiliza API_URL para eliminar con el ID correspondiente
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
      .addCase(updateInhabitant.fulfilled, (state, action) => {
        const updatedInhabitant = action.payload
        const index = state.list.findIndex(inhabitant => inhabitant.id === updatedInhabitant.id)
        if (index !== -1) {
          state.list[index] = updatedInhabitant
        }
      })
      .addCase(updateInhabitant.rejected, (state, action) => {
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
