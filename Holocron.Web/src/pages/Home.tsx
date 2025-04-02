/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useEffect, useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../redux/store'
import { fetchInhabitants, deleteInhabitant, updateInhabitant, createInhabitant } from '../redux/slices/inhabitantsSlice'
import { useNavigate } from 'react-router-dom'
import { useTranslation } from 'react-i18next'
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
  const { t } = useTranslation()
  const [editingId, setEditingId] = useState<string | null>(null)
  const [isEditing, setIsEditing] = useState(false)

  const [selectedId, setSelectedId] = useState<string | null>(null)
  const [name, setName] = useState('')
  const [species, setSpecies] = useState('')
  const [origin, setOrigin] = useState('')
  const [isRebel, setIsRebel] = useState(false)

  const [isSaveButtonActive, setIsSaveButtonActive] = useState(false)

  useEffect(() => {
    dispatch(fetchInhabitants())
  }, [dispatch])

  const handleAddClick = () => {
    navigate('/add-inhabitant')
  }

  const handleRowClick = (id: string) => {
    setSelectedId(selectedId === id ? null : id)
  }

  const handleDeleteClick = () => {
    if (selectedId !== null) {
      toastr.confirm(t('notifications.deleteConfirm'), {
        onOk: () => {
          dispatch(deleteInhabitant(selectedId))
          setSelectedId(null)
        }
      })
    } else {
      toastr.warning(t('notifications.selectToDelete'))
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
        setEditingId(inhabitant.id)
        setIsEditing(true)
        setIsSaveButtonActive(true)  // Activamos el botón Guardar
      }
    } else {
      toastr.warning(t('notifications.selectToEdit'))
    }
  }

  const handleSaveClick = async () => {
    if (editingId !== null) {
      const inhabitant = list.find((inhabitant) => inhabitant.id === editingId)
      if (inhabitant) {
        const updatedInhabitant = {
          name: name || inhabitant.name,
          species: species || inhabitant.species,
          origin: origin || inhabitant.origin,
          isSuspectedRebel: isRebel
        }

        dispatch(updateInhabitant({ id: inhabitant.id, inhabitantData: updatedInhabitant }))
        toastr.success(t('notifications.updateSuccess'))
        dispatch(fetchInhabitants())
        setEditingId(null)
        setIsEditing(false)
        setIsSaveButtonActive(false)  // Desactivamos el botón Guardar después de guardar
        setSelectedId(null)
      }
    }
  }


  return (
    <div className={classes.container}>
      <h1>{t('home.title')}</h1>

      {loading && <p>{t('common.loading')}</p>}
      {error && <p style={{ color: 'red' }}>{t('common.error', { message: error })}</p>}

      <div className={classes.listContainer}>
        <table className={classes.table}>
          <thead>
            <tr>
              <th className={classes.th}>{t('inhabitant.id')}</th>
              <th className={classes.th}>{t('inhabitant.name')}</th>
              <th className={classes.th}>{t('inhabitant.species')}</th>
              <th className={classes.th}>{t('inhabitant.rebel')}</th>
              <th className={classes.th}>{t('inhabitant.origin')}</th>
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
                <td className={classes.td}>{inhabitant.isSuspectedRebel ? t('common.yes') : t('common.no')}</td>
                <td className={classes.td}>{inhabitant.origin}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className={classes.buttonContainer}>
        <button className={classes.button} onClick={handleAddClick}>{t('buttons.add')}</button>
        <button className={classes.button} onClick={handleDeleteClick}>{t('buttons.delete')}</button>
        <button className={classes.button} onClick={handleEditClick}>{t('buttons.edit')}</button>
      </div>

      {selectedId !== null && (
        <div className={classes.formContainer}>
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.name')}
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.species')}
            value={species}
            onChange={(e) => setSpecies(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.origin')}
            value={origin}
            onChange={(e) => setOrigin(e.target.value)}
          />
          <label className={classes.checkbox}>
            <input
              type="checkbox"
              checked={isRebel}
              onChange={() => setIsRebel(!isRebel)}
            />
            {t('inhabitant.rebel')}
          </label>
          <button className={classes.button} onClick={handleSaveClick} disabled={!isSaveButtonActive}>
            {t('buttons.save')}
          </button>
        </div>
      )}

      {selectedId === null && (
        <div className={classes.formContainer}>
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.name')}
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.species')}
            value={species}
            onChange={(e) => setSpecies(e.target.value)}
          />
          <input
            className={classes.input}
            type="text"
            placeholder={t('inhabitant.origin')}
            value={origin}
            onChange={(e) => setOrigin(e.target.value)}
          />
          <label className={classes.checkbox}>
            <input
              type="checkbox"
              checked={isRebel}
              onChange={() => setIsRebel(!isRebel)}
            />
            {t('inhabitant.rebel')}
          </label>
        </div>
      )}
    </div>
  )
}

export default Home



