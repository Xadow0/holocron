import React, { useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useDispatch, useSelector } from 'react-redux'
import { AppDispatch, RootState } from '../redux/store'
import { fetchSearchResults } from '../redux/slices/inhabitantsSlice'

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
  const dispatch = useDispatch<AppDispatch>()
  const [name, setName] = useState('')
  const { searchResults, loading } = useSelector((state: RootState) => state.inhabitants)

  const handleSearch = async () => {
    const query = name.trim()
    if (query) {
      dispatch(fetchSearchResults(query))
    }
  }

  return (
    <div className={classes.container}>
      <h1>Buscar Habitantes</h1>
      <form className={classes.form} onSubmit={(e) => e.preventDefault()}>
        <input
          className={classes.input}
          type="text"
          placeholder="Nombre"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <div className={classes.buttonContainer}>
          <button
            type="button"
            className={classes.button}
            onClick={handleSearch}
            disabled={loading}
          >
            {loading ? 'Buscando...' : 'Buscar'}
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
            {searchResults.length > 0 ? (
              searchResults.map((result) => (
                <tr key={result.id}>
                  <td className={classes.td}>{result.id}</td>
                  <td className={classes.td}>{result.name}</td>
                  <td className={classes.td}>{result.species}</td>
                  <td className={classes.td}>{result.IsSuspectedRebel ? 'Sí' : 'No'}</td>
                  <td className={classes.td}>{result.origin || 'Desconocido'}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td className={classes.td} colSpan={5}>
                                    No hay resultados para mostrar
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Search

