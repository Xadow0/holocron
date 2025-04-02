import React from 'react'
import { createUseStyles } from 'react-jss'
import { useTranslation } from 'react-i18next'

const useStyles = createUseStyles({
  switcher: {
    display: 'flex',
    alignItems: 'center',
    gap: '10px'
  },
  languageButton: {
    padding: '5px 10px',
    cursor: 'pointer',
    borderRadius: '4px',
    border: '1px solid #ccc',
    backgroundColor: 'white',
    '&.active': {
      backgroundColor: '#007bff',
      color: 'white',
      border: '1px solid #0069d9'
    },
    '&:hover': {
      backgroundColor: '#f0f0f0'
    },
    '&.active:hover': {
      backgroundColor: '#0069d9'
    }
  }
})

const LanguageSwitcher: React.FC = () => {
  const classes = useStyles()
  const { i18n } = useTranslation()

  const changeLanguage = (lng: string) => {
    i18n.changeLanguage(lng)
  }

  return (
    <div className={classes.switcher}>
      <button
        className={`${classes.languageButton} ${i18n.language === 'es' ? 'active' : ''}`}
        onClick={() => changeLanguage('es')}
      >
                ES
      </button>
      <button
        className={`${classes.languageButton} ${i18n.language === 'en' ? 'active' : ''}`}
        onClick={() => changeLanguage('en')}
      >
                EN
      </button>
    </div>
  )
}

export default LanguageSwitcher
