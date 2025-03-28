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
  checkboxContainer: {
    display: 'flex',
    alignItems: 'center',
    gap: '10px'
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
  const [isRebel, setIsRebel] = useState(false)

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
        <div className={classes.checkboxContainer}>
          <label>
            <input
              type="checkbox"
              checked={isRebel}
              onChange={e => setIsRebel(e.target.checked)}
            />
                        ¿Sospechoso de ser rebelde?
          </label>
        </div>
        <button className={classes.button}>Buscar</button>
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
            <tr>
              <td className={classes.td}>-</td>
              <td className={classes.td}>-</td>
              <td className={classes.td}>-</td>
              <td className={classes.td}>-</td>
              <td className={classes.td}>-</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Search

