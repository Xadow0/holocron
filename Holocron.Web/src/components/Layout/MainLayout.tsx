import React from 'react'
import { createUseStyles } from 'react-jss'
import Sidebar from './Sidebar'

const useStyles = createUseStyles({
  layout: {
    display: 'flex',
    height: '100vh'
  },
  content: {
    flex: 1,
    padding: '20px',
    overflowY: 'auto'
  }
})

interface MainLayoutProps {
    children: React.ReactNode
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const classes = useStyles()

  return (
    <div className={classes.layout}>
      <Sidebar />
      <main className={classes.content}>
        {children}
      </main>
    </div>
  )
}

export default MainLayout