import { configureStore } from '@reduxjs/toolkit'
import inhabitantsReducer from './slices/inhabitantsSlice'
import planetsReducer from './slices/planetsSlice'

export const store = configureStore({
  reducer: {
    inhabitants: inhabitantsReducer,
    planets: planetsReducer
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store