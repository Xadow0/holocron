import React, { useState } from 'react'
import { createUseStyles } from 'react-jss'

const useStyles = createUseStyles({
  container: {
    padding: '20px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  },
  form: {
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
    width: '300px'
  },
  buttonContainer: {
    display: 'flex',
    gap: '10px',
    justifyContent: 'center'
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse'
  },
  th: {
    borderBottom: '2px solid #ccc',
    padding: '8px',
    textAlign: 'left'
  },
  td: {
    borderBottom: '1px solid #ccc',
    padding: '8px'
  },
  input: {
    padding: '10px',
    border: '1px solid #ccc',
    borderRadius: '5px'
  },
  button: {
    padding: '10px 20px',
    cursor: 'pointer',
    marginTop: '10px'
  },
  resultContainer: {
    width: '600px',
    height: '250px',
    overflowY: 'auto',
    border: '1px solid #ccc',
    padding: '10px',
    marginTop: '20px',
    display: 'flex',
    flexDirection: 'column'
  },
  resultLabel: {
    fontWeight: 'bold',
    marginBottom: '5px'
  }
})

const Search: React.FC = () => {
  const classes = useStyles()
  const [name, setName] = useState('')
  const [species, setSpecies] = useState('')
  const [rebels, setRebels] = useState<any[]>([])
  const [searchResults, setSearchResults] = useState<any[]>([])

  // Función para consultar los rebeldes (simulación de la llamada API)
  const handleConsultRebels = () => {
    const rebelList = [
      { id: 1, name: 'Luke Skywalker', species: 'Humano', isRebel: true, origin: 'Tatooine' },
      { id: 2, name: 'Leia Organa', species: 'Humano', isRebel: true, origin: 'Alderaan' },
      { id: 3, name: 'Han Solo', species: 'Humano', isRebel: true, origin: 'Corellia' }
    ]
    setRebels(rebelList)
    setSearchResults([]) // Limpiar resultados de búsqueda si se consulta rebeldes
  }

  // Función para buscar por nombre y especie
  const handleSearch = () => {
    // Aquí puedes agregar la lógica de búsqueda real, por ejemplo, filtrando por nombre o especie
    const filteredResults = [
      { id: 1, name: 'Han Solo', species: 'Humano', isRebel: true, origin: 'Corellia' }
    ]
    setSearchResults(filteredResults)
    setRebels([]) // Limpiar rebeldes si se realiza una búsqueda
  }

  return (
    <div className={classes.container}>
      <h1>Buscar Habitantes</h1>
      <form className={classes.form}>
        <input
          className={classes.input}
          type="text"
          placeholder="Nombre"
          value={name}
          onChange={e => setName(e.target.value)}
        />
        <input
          className={classes.input}
          type="text"
          placeholder="Especie"
          value={species}
          onChange={e => setSpecies(e.target.value)}
        />
        <div className={classes.buttonContainer}>
          <button
            type="button"
            className={classes.button}
            onClick={handleSearch}
          >
                        Buscar
          </button>
          <button
            type="button"
            className={classes.button}
            onClick={handleConsultRebels}
          >
                        Consultar Rebeldes
          </button>
        </div>
      </form>
      <div className={classes.resultContainer}>
        <span className={classes.resultLabel}>Resultado</span>
        <table className={classes.table}>
          <thead>
            <tr>
              <th className={classes.th}>ID</th>
              <th className={classes.th}>Nombre</th>
              <th className={classes.th}>Especie</th>
              <th className={classes.th}>Rebelde</th>
              <th className={classes.th}>Origen</th>
            </tr>
          </thead>
          <tbody>
            {rebels.length > 0 ? (
              rebels.map(rebel => (
                <tr key={rebel.id}>
                  <td className={classes.td}>{rebel.id}</td>
                  <td className={classes.td}>{rebel.name}</td>
                  <td className={classes.td}>{rebel.species}</td>
                  <td className={classes.td}>{rebel.isRebel ? 'Si' : 'No'}</td>
                  <td className={classes.td}>{rebel.origin}</td>
                </tr>
              ))
            ) : searchResults.length > 0 ? (
              searchResults.map(result => (
                <tr key={result.id}>
                  <td className={classes.td}>{result.id}</td>
                  <td className={classes.td}>{result.name}</td>
                  <td className={classes.td}>{result.species}</td>
                  <td className={classes.td}>{result.isRebel ? 'Si' : 'No'}</td>
                  <td className={classes.td}>{result.origin}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td className={classes.td} colSpan={5}>No hay resultados para mostrar</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Search
