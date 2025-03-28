import React from 'react'
import { createUseStyles } from 'react-jss'
import { Link } from 'react-router-dom'

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

  return (
    <aside className={classes.sidebar}>
      <h2>Proyecto Holocron</h2>
      <nav>
        <Link to="/" className={classes.navLink}>Inicio</Link>
        <Link to="/inhabitants" className={classes.navLink}>Habitantes</Link>
      </nav>
    </aside>
  )
}

export default Sidebar