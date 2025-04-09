using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Domain.DTOs.External;

namespace Holocron.Domain.Interfaces.Services
{
    public interface IPlanetService
    {
        Task<List<PlanetDto>> GetAllPlanetsAsync();
    }
}
