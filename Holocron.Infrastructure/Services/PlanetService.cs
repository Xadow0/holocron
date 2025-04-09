using Holocron.Domain.DTOs.External;
using Holocron.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Holocron.Infrastructure.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly HttpClient _httpClient;
        public PlanetService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlanetDto>> GetAllPlanetsAsync()
        {
            var response = await _httpClient.GetAsync("https://swapi.dev/api/planets/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<SwapiPlanetResponse>(json);

            return root?.Results.Select(p => new PlanetDto
            {
                Name = p.Name,
                Climate = p.Climate,
                Terrain = p.Terrain,
                Population = p.Population
            }).ToList() ?? new List<PlanetDto>();
        }

        private class SwapiPlanetResponse
        {
            public List<PlanetDto> Results { get; set; }
        }
    }
}

