using System.Text.Json;
using Holocron.Domain.Entities;
using Holocron.Infrastructure.Services.Models;

namespace Holocron.Infrastructure.Services
{
    public class SwapiService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://swapi.dev/api/";

        public SwapiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BASE_URL);
        }

        public async Task<List<Planet>> GetAllPlanetsAsync()
        {
            var allPlanets = new List<Planet>();
            string? nextPage = "https://swapi.dev/api/planets/";

            try
            {
                while (!string.IsNullOrEmpty(nextPage))
                {
                    var response = await _httpClient.GetAsync(nextPage);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var planetResponse = JsonSerializer.Deserialize<SwapiPlanetResponse>(content);

                    if (planetResponse == null)
                        break;

                    // Map SWAPI planets to our domain model
                    var planets = planetResponse.Results.Select(p => MapToPlanet(p));
                    allPlanets.AddRange(planets);

                    // Usa directamente la URL completa proporcionada por la API
                    nextPage = planetResponse.Next;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores básico (puedes mejorarlo según tu necesidad)
                Console.WriteLine($"Error fetching planets: {ex.Message}");
            }

            return allPlanets;
        }


        public async Task<Planet?> GetPlanetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"planets/{id}/");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var swapiPlanet = JsonSerializer.Deserialize<SwapiPlanet>(content);

            return swapiPlanet != null ? MapToPlanet(swapiPlanet) : null;
        }

        public async Task<List<Planet>> SearchPlanetsAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"planets/?search={searchTerm}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var planetResponse = JsonSerializer.Deserialize<SwapiPlanetResponse>(content);

            if (planetResponse == null)
                return new List<Planet>();

            return planetResponse.Results.Select(p => MapToPlanet(p)).ToList();
        }

        private Planet MapToPlanet(SwapiPlanet swapiPlanet)
        {
            // Extract ID from URL
            var idStr = swapiPlanet.Url.TrimEnd('/').Split('/').LastOrDefault();
            var id = 0;

            if (idStr != null)
                int.TryParse(idStr, out id);

            return new Planet
            {
                Id = id,
                Name = swapiPlanet.Name,
                Climate = swapiPlanet.Climate,
                Terrain = swapiPlanet.Terrain,
                Population = swapiPlanet.Population,
                Diameter = swapiPlanet.Diameter,
                Gravity = swapiPlanet.Gravity,
                OrbitalPeriod = swapiPlanet.OrbitalPeriod,
                RotationPeriod = swapiPlanet.RotationPeriod,
                SurfaceWater = swapiPlanet.SurfaceWater,
                Url = swapiPlanet.Url,
                Created = swapiPlanet.Created,
                Edited = swapiPlanet.Edited
            };
        }
    }
}
