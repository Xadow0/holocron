using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Holocron.Domain.Entities;

namespace Holocron.Domain.Interfaces.Repositories
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllPlanetsAsync();
        Task<Planet?> GetPlanetByIdAsync(int id);
        Task<IEnumerable<Planet>> SearchPlanetsAsync(string searchTerm);
    }
}