import axios from 'axios'
import type { Planet } from '../types/planets'

//const API_BASE_URL = 'https://lsanwebapp01-ahdhasdffjczcehe.spaincentral-01.azurewebsites.net/api' // Backend URL
const API_BASE_URL = 'https://localhost:7278/api'

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

export const getPlanets = async () => {
  const response = await api.get('/planets')
  return response.data
}

export const getPlanetById = async (id: number): Promise<Planet | null> => {
  try {
    const response = await api.get<Planet>(`/planets/${id}`)
    return response.data
  } catch (error) {
    console.error(`Error fetching planet with ID ${id}:`, error)
    return null
  }
}

export const searchPlanets = async (term: string): Promise<Planet[]> => {
  try {
    const response = await api.get<Planet[]>('/planets/search', {
      params: { term },
    })
    return response.data
  } catch (error) {
    console.error('Error searching planets:', error)
    return []
  }
}

export default api

