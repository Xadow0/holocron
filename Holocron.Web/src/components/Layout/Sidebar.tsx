import React from 'react'
import { createUseStyles } from 'react-jss'
import { Link } from 'react-router-dom'
import { useTranslation } from 'react-i18next'

const useStyles = createUseStyles({
  sidebar: {
    width: '250px',
    backgroundColor: '#2c3e50',
    color: 'white',
    padding: '20px',
    display: 'flex',
    flexDirection: 'column'
  },
  navLink: {
    color: 'white',
    textDecoration: 'none',
    padding: '10px',
    '&:hover': {
      backgroundColor: '#34495e'
    }
  }
})

const Sidebar: React.FC = () => {
  const classes = useStyles()
  const { t } = useTranslation()

  return (
    <aside className={classes.sidebar}>
      <h2>{t('common.project')}</h2>
      <nav>
        <Link to="/" className={classes.navLink}>{t('common.start')}</Link>
        <Link to="/inhabitants" className={classes.navLink}>{t('common.habitants')}</Link>
      </nav>
    </aside>
  )
}

export default Sidebar