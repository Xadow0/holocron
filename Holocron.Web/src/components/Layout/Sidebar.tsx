import React from 'react'
import { createUseStyles } from 'react-jss'
import { Link } from 'react-router-dom'
import { useTranslation } from 'react-i18next'

const useStyles = createUseStyles({
  sidebar: {
    width: '250px',
    backgroundColor: '#13171C',
    color: 'white',
    padding: '20px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  },
  logo: {
    width: '100px',  // Logo width
    height: 'auto',
    marginBottom: '10px', // Space between logo and title
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
      <img src="/logo.png" alt="Logotipo" className={classes.logo} />
      <h2>{t('common.project')}</h2>
      <nav>
        <Link to="/" className={classes.navLink}>{t('common.start')}</Link>
        <Link to="/inhabitants" className={classes.navLink}>{t('common.habitants')}</Link>
        <Link to="/planets" className={classes.navLink}>{t('common.planets')}</Link>
      </nav>
    </aside>
  )
}

export default Sidebar
