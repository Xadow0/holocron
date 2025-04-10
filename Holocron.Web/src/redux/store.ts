import { configureStore } from '@reduxjs/toolkit'
import inhabitantsReducer from './slices/inhabitantsSlice'
import planetsReducer from './slices/planetsSlice'

// Configuración del store
export const store = configureStore({
  reducer: {
    inhabitants: inhabitantsReducer,
    planets: planetsReducer
  }
})

// Tipos del estado global y del dispatcher para usar en los hooks
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store
