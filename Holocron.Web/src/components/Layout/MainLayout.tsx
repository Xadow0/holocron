import React from 'react'
import { createUseStyles } from 'react-jss'
import Sidebar from './Sidebar'
import LanguageSwitcher from './LanguageSwitcher'

const useStyles = createUseStyles({
  layout: {
    display: 'flex',
    height: '100vh'
  },
  content: {
    flex: 1,
    padding: '20px',
    overflowY: 'auto'
  },
  header: {
    display: 'flex',
    justifyContent: 'flex-end',
    padding: '10px'
  }
})

interface MainLayoutProps {
    children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const classes = useStyles()

  return (
    <div className={classes.layout}>
      <Sidebar />
      <main className={classes.content}>
        <div className={classes.header}>
          <LanguageSwitcher />
        </div>
        {children}
      </main>
    </div>
  )
}

export default MainLayout