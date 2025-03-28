import React from 'react'
import { createUseStyles } from 'react-jss'

const useStyles = createUseStyles({
    container: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100%',
        textAlign: 'center'
    },
    title: {
        fontSize: '2rem',
        color: '#333'
    }
})

const Home: React.FC = () => {
    const classes = useStyles()

    return (
        <div className={classes.container}>
            <h1 className={classes.title}>Bienvenido a Holocron</h1>
        </div>
    )
}

export default Home