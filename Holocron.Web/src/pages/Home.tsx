// Home.tsx
import React from 'react'
import { createUseStyles } from 'react-jss'
import { useSelector } from 'react-redux'
import { RootState } from '../redux/store'
import { useNavigate } from 'react-router-dom' // Importa useNavigate

const useStyles = createUseStyles({
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: '20px'
  },
  listContainer: {
    width: '600px',
    height: '250px',
    overflowY: 'auto',
    border: '1px solid #ccc',
    padding: '10px',
    marginBottom: '20px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
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
  buttonContainer: {
    display: 'flex',
    gap: '10px'
  },
  button: {
    padding: '10px 20px',
    cursor: 'pointer'
  }
})

const Home: React.FC = () => {
  const classes = useStyles()
  const { list } = useSelector((state: RootState) => state.inhabitants)
  const navigate = useNavigate() // Inicializa el hook de navegación

  const handleAddClick = () => {
    navigate('/add-inhabitant') // Redirige a la página de agregar habitante
  }

  return (
    <div className={classes.container}>
      <h1>Lista de habitantes de Tatooine</h1>
      <div className={classes.listContainer}>
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
            {list.map(inhabitant => (
              <tr key={inhabitant.id}>
                <td className={classes.td}>{inhabitant.id}</td>
                <td className={classes.td}>{inhabitant.name}</td>
                <td className={classes.td}>{inhabitant.species}</td>
                <td className={classes.td}>{inhabitant.IsSuspectedRebel ? 'Sí' : 'No'}</td>
                <td className={classes.td}>{inhabitant.origin}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className={classes.buttonContainer}>
        <button className={classes.button} onClick={handleAddClick}>Agregar</button> {/* Llama a handleAddClick */}
        <button className={classes.button}>Eliminar</button>
        <button className={classes.button}>Editar</button>
      </div>
    </div>
  )
}

export default Home
