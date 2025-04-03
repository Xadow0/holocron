import axios from 'axios'

const API_BASE_URL = 'https://lsanwebapp01-ahdhasdffjczcehe.spaincentral-01.azurewebsites.net/api' // Backend URL

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

export default api
