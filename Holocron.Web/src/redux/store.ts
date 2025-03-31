import { configureStore } from '@reduxjs/toolkit'
import inhabitantsReducer from './slices/inhabitantsSlice'

export const store = configureStore({
  reducer: {
    inhabitants: inhabitantsReducer
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store