// AddInhabitant.tsx
import React, { useState } from 'react'
import { createUseStyles } from 'react-jss'

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

  // Estados para los campos
  const [name, setName] = useState('')
  const [species, setSpecies] = useState('')
  const [origin, setOrigin] = useState('')
  const [isRebel, setIsRebel] = useState(false)

  // Validaci�n para habilitar o deshabilitar el bot�n
  const isFormValid = name.trim() !== '' && species.trim() !== ''

  // Funci�n para manejar el env�o del formulario
  const handleSave = () => {
    // Aqu� puedes agregar la l�gica para guardar el nuevo habitante
    console.log({ name, species, origin, isRebel })
  }

  return (
    <div className={classes.container}>
      <h1>Agregar Nuevo Habitante</h1>

      {/* Campo para el nombre */}
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

      {/* Campo para la especie */}
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

      {/* Campo para el origen */}
      <input
        className={classes.input}
        type="text"
        placeholder="Origen (opcional)"
        value={origin}
        onChange={(e) => setOrigin(e.target.value)}
      />

      {/* Checkbox para "Rebelde" */}
      <label>
        <input
          type="checkbox"
          checked={isRebel}
          onChange={(e) => setIsRebel(e.target.checked)}
        />
                Rebelde
      </label>

      {/* Texto que aparece cuando el formulario es inv�lido */}
      {!isFormValid && (
        <div className={classes.errorText}>
                    Los campos <strong>Nombre</strong> y <strong>Especie</strong> son obligatorios
        </div>
      )}

      {/* Bot�n de Guardar, deshabilitado si no es v�lido */}
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
