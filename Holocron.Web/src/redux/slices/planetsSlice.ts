import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { getPlanets, getPlanetById, searchPlanets } from '../../utils/api'
import { Planet } from '../../types/planets'

interface PlanetsState
{
    planets: Planet[];
  selectedPlanet: Planet | null;
  loading: boolean;
  error: string | null;
}

const initialState: PlanetsState = {
  planets: [],
  selectedPlanet: null,
  loading: false,
  error: null,
}


export const fetchPlanets = createAsyncThunk(
  'planets/fetchAll',
  async () => {
    return await getPlanets()
  }
)

export const fetchPlanetById = createAsyncThunk(
  'planets/fetchById',
  async (id: number) => {
    return await getPlanetById(id)
  }
)

export const searchPlanetsThunk = createAsyncThunk(
  'planets/search',
  async (term: string) => {
    return await searchPlanets(term)
  }
)

const planetsSlice = createSlice({
  name: 'planets',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
    // Fetch all planets
      .addCase(fetchPlanets.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchPlanets.fulfilled, (state, action) => {
        state.planets = action.payload
        state.loading = false
      })
      .addCase(fetchPlanets.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to fetch planets'
      })

    // Fetch planet by ID
      .addCase(fetchPlanetById.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchPlanetById.fulfilled, (state, action) => {
        state.selectedPlanet = action.payload
        state.loading = false
      })
      .addCase(fetchPlanetById.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to fetch planet'
      })

    // Search planets
      .addCase(searchPlanetsThunk.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(searchPlanetsThunk.fulfilled, (state, action) => {
        state.planets = action.payload
        state.loading = false
      })
      .addCase(searchPlanetsThunk.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to search planets'
      })
  },
})

export default planetsSlice.reducer
