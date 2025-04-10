import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { fetchPlanets } from '../redux/slices/planetsSlice'
import { RootState } from '../redux/store'
import { Orbit, Users, Cloud, Mountain } from 'lucide-react'

const Planets = () => {
  const dispatch = useDispatch()
  const { planets, loading, error } = useSelector((state: RootState) => state.planets)

  useEffect(() => {
    dispatch(fetchPlanets())
  }, [dispatch])

  if (loading) {
    return (
      <div className="p-6 bg-gray-900 min-h-screen text-white flex justify-center items-center">
        <div className="text-2xl">Loading planets...</div>
      </div>
    )
  }

  if (error) {
    return (
      <div className="p-6 bg-gray-900 min-h-screen text-white flex justify-center items-center">
        <div className="text-2xl text-red-500">Error: {error}</div>
      </div>
    )
  }

  return (
    <div className="p-6 bg-gray-900 min-h-screen text-white">
      <h2 className="text-4xl font-bold mb-10 text-center">Galactic Atlas</h2>

      <div className="overflow-x-auto rounded-xl shadow-2xl border border-gray-700">
        <table className="min-w-full bg-gray-800 rounded-xl">
          <thead className="bg-gray-700 text-white">
            <tr>
              <th className="px-6 py-4 text-left text-sm font-semibold tracking-wider">
                <div className="flex items-center gap-2"><Orbit className="w-5 h-5 text-yellow-400" />Planet</div>
              </th>
              <th className="px-6 py-4 text-left text-sm font-semibold tracking-wider">
                <div className="flex items-center gap-2"><Cloud className="w-5 h-5 text-blue-400" />Climate</div>
              </th>
              <th className="px-6 py-4 text-left text-sm font-semibold tracking-wider">
                <div className="flex items-center gap-2"><Mountain className="w-5 h-5 text-green-400" />Terrain</div>
              </th>
              <th className="px-6 py-4 text-left text-sm font-semibold tracking-wider">
                <div className="flex items-center gap-2"><Users className="w-5 h-5 text-pink-400" />Population</div>
              </th>
            </tr>
          </thead>
          <tbody>
            {planets.map((planet, index) => (
              <tr
                key={index}
                className="border-b border-gray-600 hover:bg-gray-700 transition-colors"
              >
                <td className="px-6 py-6 text-sm font-medium">{planet.name}</td>
                <td className="px-6 py-6 text-sm text-gray-300">{planet.climate}</td>
                <td className="px-6 py-6 text-sm text-gray-300">{planet.terrain}</td>
                <td className="px-6 py-6 text-sm text-gray-300">{planet.population}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Planets




