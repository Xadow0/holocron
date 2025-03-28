import React from 'react'
import { createUseStyles } from 'react-jss'
import { useSelector, useDispatch } from 'react-redux'
import { RootState, AppDispatch } from '../redux/store'
import { setPage } from '../redux/slices/inhabitantsSlice'

const useStyles = createUseStyles({
  container: {
    padding: '20px'
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse'
  },
  pagination: {
    display: 'flex',
    justifyContent: 'center',
    marginTop: '20px'
  }
})

const Inhabitants: React.FC = () => {
  const classes = useStyles()
  const dispatch: AppDispatch = useDispatch()
  const { list, page, totalPages } = useSelector((state: RootState) => state.inhabitants)

  const handlePageChange = (newPage: number) => {
    dispatch(setPage(newPage))
  }

  return (
    <div className={classes.container}>
      <h1>Habitantes</h1>
      <table className={classes.table}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
          </tr>
        </thead>
        <tbody>
          {list.map(inhabitant => (
            <tr key={inhabitant.id}>
              <td>{inhabitant.id}</td>
              <td>{inhabitant.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className={classes.pagination}>
        {Array.from({ length: totalPages }, (_, i) => i + 1).map(pageNumber => (
          <button
            key={pageNumber}
            onClick={() => handlePageChange(pageNumber)}
            disabled={pageNumber === page}
          >
            {pageNumber}
          </button>
        ))}
      </div>
    </div>
  )
}

export default Inhabitants