import React, { useEffect, useState } from 'react'
import { getPlanets } from '../utils/api'
import { motion } from 'framer-motion'
import { Orbit, Users, Cloud, Mountain } from 'lucide-react'

interface Planet {
    name: string;
    climate: string;
    terrain: string;
    population: string;
}

const cardVariants = {
  hidden: { opacity: 0, scale: 0.9 },
  visible: (i: number) => ({
    opacity: 1,
    scale: 1,
    transition: { delay: i * 0.1 },
  }),
}

const Planets = () => {
  const [planets, setPlanets] = useState<Planet[]>([])

  useEffect(() => {
    getPlanets().then(setPlanets)
  }, [])

  return (
    <div className="p-6 bg-gray-900 min-h-screen text-white">
      <h2 className="text-4xl font-bold mb-6 text-center">Galactic Atlas</h2>
      <div className="grid gap-6 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
        {planets.map((planet, index) => (
          <motion.div
            key={index}
            className="bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-2xl shadow-xl hover:shadow-2xl hover:scale-[1.02] transition-all duration-300 cursor-pointer border border-gray-600"
            custom={index}
            variants={cardVariants}
            initial="hidden"
            animate="visible"
            whileHover={{ rotate: 0.5 }}
          >
            <div className="flex items-center mb-2">
              <Orbit className="text-yellow-400 mr-2" />
              <h3 className="text-xl font-semibold">{planet.name}</h3>
            </div>
            <div className="flex items-center mb-1">
              <Cloud className="text-blue-400 mr-2" />
              <p className="text-sm text-gray-300">Climate: {planet.climate}</p>
            </div>
            <div className="flex items-center mb-1">
              <Mountain className="text-green-400 mr-2" />
              <p className="text-sm text-gray-300">Terrain: {planet.terrain}</p>
            </div>
            <div className="flex items-center">
              <Users className="text-pink-400 mr-2" />
              <p className="text-sm text-gray-300">Population: {planet.population}</p>
            </div>
          </motion.div>
        ))}
      </div>
    </div>
  )
}

export default Planets
