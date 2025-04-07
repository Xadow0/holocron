import React, { useState } from 'react'
import { createUseStyles } from 'react-jss'
import { useDispatch, useSelector } from 'react-redux'
import { AppDispatch, RootState } from '../redux/store'
import { fetchSearchResults, fetchRebels } from '../redux/slices/inhabitantsSlice'
import { useTranslation } from 'react-i18next'

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
  const { t } = useTranslation()

  const handleSearch = async () => {
    const query = name.trim()
    if (query) {
      dispatch(fetchSearchResults(query))
    }
  }

  const handleShowRebels = () => {
    dispatch(fetchRebels())
  }

  return (
    <div className={classes.container}>
      <h1>{t('search.title')}</h1>
      <form className={classes.form} onSubmit={(e) => e.preventDefault()}>
        <input
          className={classes.input}
          type="text"
          placeholder={t('search.placeholder')}
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
            {loading ? t('buttons.searchingText') : t('buttons.search')}
          </button>
          <button
            type="button"
            className={classes.button}
            onClick={handleShowRebels}
            disabled={loading}
          >
            {loading ? t('buttons.loadingRebels') : t('buttons.showRebels')}
          </button>
        </div>
      </form>
      <div className={classes.resultContainer}>
        <span className={classes.resultLabel}>{t('search.result')}</span>
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
            {searchResults.length > 0 ? (
              searchResults.map((result) => (
                <tr key={result.id}>
                  <td className={classes.td}>{result.id}</td>
                  <td className={classes.td}>{result.name}</td>
                  <td className={classes.td}>{result.species}</td>
                  <td className={classes.td}>{result.isSuspectedRebel ? t('common.yes') : t('common.no')}</td>
                  <td className={classes.td}>{result.origin || t('common.unknown')}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td className={classes.td} colSpan={5}>
                  {t('inhabitant.noResults')}
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
