import { createUseStyles } from 'react-jss'

const useGlobalStyles = createUseStyles({
  '@global': {
    body: {
      margin: 0,
      padding: 0,
      fontFamily: '\'Segoe UI\', Tahoma, Geneva, Verdana, sans-serif',
      backgroundColor: '#f4f4f4',
      color: '#333'
    },
    '*': {
      boxSizing: 'border-box'
    },
    a: {
      textDecoration: 'none',
      color: '#007bff',
      transition: 'color 0.3s ease'
    },
    'a:hover': {
      color: '#0056b3'
    },
    button: {
      cursor: 'pointer',
      transition: 'all 0.3s ease'
    }
  }
})

export default useGlobalStyles
