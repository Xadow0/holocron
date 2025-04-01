import React, { useEffect, useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../redux/store'
import { fetchInhabitants, deleteInhabitant, updateInhabitant, createInhabitant } from '../redux/slices/inhabitantsSlice'
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
  },
  formContainer: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    marginTop: '20px'
  },
  input: {
    margin: '5px',
    padding: '8px',
    width: '200px'
  },
  checkbox: {
    margin: '5px'
  }
})

const Home: React.FC = () => {
  const classes = useStyles()
  const dispatch = useDispatch<AppDispatch>()
  const { list, loading, error } = useSelector((state: RootState) => state.inhabitants)
  const navigate = useNavigate()
  const [selectedId, setSelectedId] = useState<string | null>(null) // Cambiado a GUID (string)
  const [name, setName] = useState('')
  const [species, setSpecies] = useState('')
  const [origin, setOrigin] = useState('')
  const [isRebel, setIsRebel] = useState(false)

  useEffect(() => {
    dispatch(fetchInhabitants())
  }, [dispatch])

  const handleAddClick = () => {
    navigate('/add-inhabitant')
  }

  const handleRowClick = (id: string) => { // Cambiado a GUID (string)
    setSelectedId(selectedId === id ? null : id)
  }

  const handleDeleteClick = () => {
    if (selectedId !== null) {
      toastr.confirm('Seguro de que quieres eliminar este habitante?', {
        onOk: () => {
          dispatch(deleteInhabitant(selectedId)) // Cambiado a GUID (string)
          setSelectedId(null)
        }
      })
    } else {
      toastr.warning('Selecciona un habitante para eliminar')
    }
  }

  const handleEditClick = () => {
    if (selectedId !== null) {
      const inhabitant = list.find((inhabitant) => inhabitant.id === selectedId)
      if (inhabitant) {
        setName(inhabitant.name)
        setSpecies(inhabitant.species)
        setOrigin(inhabitant.origin)
        setIsRebel(inhabitant.isSuspectedRebel)
      }
    } else {
      toastr.warning('Selecciona un habitante para editar')
    }
  }

  const handleSaveClick = async () => {
    if (selectedId !== null) {
      const inhabitant = list.find((inhabitant) => inhabitant.id === selectedId)
      if (inhabitant) {
        const updatedInhabitant = {
          name: name || inhabitant.name,
          species: species || inhabitant.species,
          origin: origin || inhabitant.origin,
          isSuspectedRebel: isRebel
        }

        dispatch(updateInhabitant({ id: inhabitant.id, inhabitantData: updatedInhabitant })) 
        toastr.success('Habitante actualizado exitosamente')
        dispatch(fetchInhabitants())
        setSelectedId(null)
      }
    }
  }

  const handleCreateClick = async () => {
    const newInhabitant = {
      name,
      species,
      origin,
      isSuspectedRebel: isRebel
    }

    dispatch(createInhabitant(newInhabitant))
    toastr.success('Habitante creado exitosamente')
    setName('')
    setSpecies('')
    setOrigin('')
    setIsRebel(false)
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
                onClick={() => handleRowClick(inhabitant.id)} // Changed to GUID (string)
              >
                <td className={classes.td}>{inhabitant.id}</td>
                <td className={classes.td}>{inhabitant.name}</td>
                <td className={classes.td}>{inhabitant.species}</td>
                <td className={classes.td}>{inhabitant.isSuspectedRebel ? 'Si' : 'No'}</td>
                <td className={classes.td}>{inhabitant.origin}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className={classes.buttonContainer}>
        <button className={classes.button} onClick={handleAddClick}>Agregar</button>
        <button className={classes.button} onClick={handleDeleteClick}>Eliminar</button>
        <button className={classes.button} onClick={handleEditClick}>Editar</button>
      </div>

      {selectedId !== null && (
        <div className={classes.formContainer}>
          <input
            className={classes.input}
            type="text"
            placeholder="Nombre"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder="Especie"
            value={species}
            onChange={(e) => setSpecies(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder="Origen"
            value={origin}
            onChange={(e) => setOrigin(e.target.value)}
          />
          <label className={classes.checkbox}>
            <input
              type="checkbox"
              checked={isRebel}
              onChange={() => setIsRebel(!isRebel)}
            />
                        Rebelde
          </label>
          <button className={classes.button} onClick={handleSaveClick}>Guardar</button>
        </div>
      )}

      {selectedId === null && (
        <div className={classes.formContainer}>
          <input
            className={classes.input}
            type="text"
            placeholder="Nombre"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder="Especie"
            value={species}
            onChange={(e) => setSpecies(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder="Origen"
            value={origin}
            onChange={(e) => setOrigin(e.target.value)}
          />
          <label className={classes.checkbox}>
            <input
              type="checkbox"
              checked={isRebel}
              onChange={() => setIsRebel(!isRebel)}
            />
                        Rebelde
          </label>
          <button className={classes.button} onClick={handleCreateClick}>Crear</button>
        </div>
      )}
    </div>
  )
}

export default Home

