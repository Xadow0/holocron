import React, { useEffect, useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../redux/store'
import { fetchInhabitants, deleteInhabitant } from '../redux/slices/inhabitantsSlice'
import { useNavigate } from 'react-router-dom'
import toastr from '../utils/toastr'

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
  },
  selectedRow: {
    backgroundColor: '#f0f0f0'
  }
})

const Home: React.FC = () => {
  const classes = useStyles()
  const dispatch = useDispatch<AppDispatch>()
  const { list, loading, error } = useSelector((state: RootState) => state.inhabitants)
  const navigate = useNavigate()
  const [selectedId, setSelectedId] = useState<number | null>(null)

  useEffect(() => {
    dispatch(fetchInhabitants())
  }, [dispatch])

  const handleAddClick = () => {
    navigate('/add-inhabitant')
  }

  const handleRowClick = (id: number) => {
    setSelectedId(selectedId === id ? null : id)
  }

  const handleDeleteClick = () => {
    if (selectedId !== null) {
      toastr.confirm('¿Estás seguro de que quieres eliminar este habitante?', {
        onOk: () => {
          dispatch(deleteInhabitant(selectedId))
          setSelectedId(null)
        }
      })
    } else {
      toastr.warning('Selecciona un habitante para eliminar')
    }
  }

  return (
    <div className={classes.container}>
      <h1>Lista de habitantes de Tatooine</h1>

      {loading && <p>Cargando habitantes...</p>}
      {error && <p style={{ color: 'red' }}>Error: {error}</p>}

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
              <tr
                key={inhabitant.id}
                className={selectedId === inhabitant.id ? classes.selectedRow : ''}
                onClick={() => handleRowClick(inhabitant.id)}
              >
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
        <button className={classes.button} onClick={handleAddClick}>Agregar</button>
        <button className={classes.button} onClick={handleDeleteClick}>Eliminar</button>
        <button className={classes.button}>Editar</button>
      </div>
    </div>
  )
}

export default Home


