import React from 'react'
import { Routes, Route } from 'react-router-dom'
import { ThemeProvider } from 'react-jss'

import MainLayout from './components/Layout/MainLayout'
import Home from './pages/Home'
import Inhabitants from './pages/Search'
import AddInhabitant from './pages/AddInhabitant'

const theme = {
  primaryColor: '#007bff',
  backgroundColor: '#f4f4f4'
}

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <MainLayout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/inhabitants" element={<Inhabitants />} />
          <Route path="/add-inhabitant" element={<AddInhabitant />} />
        </Routes>
      </MainLayout>
    </ThemeProvider>
  )
}

export default App