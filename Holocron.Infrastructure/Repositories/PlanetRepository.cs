using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Services;

namespace Holocron.Infrastructure.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly SwapiService _swapiService;
        private List<Planet>? _cachedPlanets;
        private DateTime _lastCacheUpdate = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        public PlanetRepository(SwapiService swapiService)
        {
            _swapiService = swapiService;
        }

        public async Task<IEnumerable<Planet>> GetAllPlanetsAsync()
        {
            await UpdateCacheIfNeededAsync();
            return _cachedPlanets ?? new List<Planet>();
        }

        public async Task<Planet?> GetPlanetByIdAsync(int id)
        {
            await UpdateCacheIfNeededAsync();
            return _cachedPlanets?.FirstOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Planet>> SearchPlanetsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllPlanetsAsync();

            await UpdateCacheIfNeededAsync();

            searchTerm = searchTerm.ToLower();
            return _cachedPlanets?.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Climate.ToLower().Contains(searchTerm) ||
                p.Terrain.ToLower().Contains(searchTerm)) ?? new List<Planet>();
        }

        private async Task UpdateCacheIfNeededAsync()
        {
            // Check if cache needs to be refreshed
            if (_cachedPlanets == null || DateTime.Now - _lastCacheUpdate > _cacheExpiration)
            {
                _cachedPlanets = await _swapiService.GetAllPlanetsAsync();
                _lastCacheUpdate = DateTime.Now;
            }
        }
    }
}
