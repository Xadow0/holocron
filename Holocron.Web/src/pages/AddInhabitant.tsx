import React, { useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useDispatch } from 'react-redux'
import { AppDispatch } from '../redux/store'
import { createInhabitant } from '../redux/slices/inhabitantsSlice'
import toastr from '../utils/toastr'

const useStyles = createUseStyles({
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: '20px'
  },
  input: {
    padding: '10px',
    margin: '10px 0',
    width: '300px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  },
  button: {
    padding: '10px 20px',
    cursor: 'pointer',
    marginTop: '20px',
    backgroundColor: '#007bff',
    color: 'white',
    border: 'none',
    borderRadius: '4px',
    '&:disabled': {
      backgroundColor: '#ccc',
      cursor: 'not-allowed'
    }
  },
  requiredField: {
    color: '#ff0000',
    fontSize: '12px',
    marginTop: '5px',
  },
  requiredAsterisk: {
    color: '#ff0000',
    marginLeft: '5px'
  },
  errorText: {
    color: '#ff0000',
    fontSize: '12px',
    marginTop: '10px',
    textAlign: 'center'
  }
})

const AddInhabitant: React.FC = () => {
  const classes = useStyles()
  const dispatch = useDispatch<AppDispatch>()

  const [name, setName] = useState('')
  const [species, setSpecies] = useState('')
  const [origin, setOrigin] = useState('')
  const [isRebel, setIsRebel] = useState(false)

  // validation to activate the button
  const isFormValid = name.trim() !== '' && species.trim() !== ''

  const handleSave = async () => {
    try {
      await dispatch(createInhabitant({ name, species, origin, isSuspectedRebel: isRebel })).unwrap()
      toastr.success('Habitante creado exitosamente')
      setName('')
      setSpecies('')
      setOrigin('')
      setIsRebel(false)
    } catch (error) {
      toastr.error('Error al crear el habitante')
    }
  }

  return (
    <div className={classes.container}>
      <h1>Agregar Nuevo Habitante</h1>

      <div>
        <input
          className={classes.input}
          type="text"
          placeholder="Nombre"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <span className={classes.requiredAsterisk}>*</span>
      </div>

      <div>
        <input
          className={classes.input}
          type="text"
          placeholder="Especie"
          value={species}
          onChange={(e) => setSpecies(e.target.value)}
        />
        <span className={classes.requiredAsterisk}>*</span>
      </div>

      <input
        className={classes.input}
        type="text"
        placeholder="Origen (opcional)"
        value={origin}
        onChange={(e) => setOrigin(e.target.value)}
      />

      <label>
        <input
          type="checkbox"
          checked={isRebel}
          onChange={(e) => setIsRebel(e.target.checked)}
        />
                Rebelde
      </label>

      {/* Text when the form is invalid */}
      {!isFormValid && (
        <div className={classes.errorText}>
                    Los campos <strong>Nombre</strong> y <strong>Especie</strong> son obligatorios
        </div>
      )}

      {/* Disable save button */}
      <button
        className={classes.button}
        onClick={handleSave}
        disabled={!isFormValid}
      >
                Guardar
      </button>
    </div>
  )
}

export default AddInhabitant
