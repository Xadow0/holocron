using System.Text.Json.Serialization;

namespace Holocron.Infrastructure.Services.Models
{
    public class SwapiPlanetResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("previous")]
        public string? Previous { get; set; }

        [JsonPropertyName("results")]
        public List<SwapiPlanet> Results { get; set; } = new();
    }

    public class SwapiPlanet
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; set; } = string.Empty;

        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; set; } = string.Empty;

        [JsonPropertyName("diameter")]
        public string Diameter { get; set; } = string.Empty;

        [JsonPropertyName("climate")]
        public string Climate { get; set; } = string.Empty;

        [JsonPropertyName("gravity")]
        public string Gravity { get; set; } = string.Empty;

        [JsonPropertyName("terrain")]
        public string Terrain { get; set; } = string.Empty;

        [JsonPropertyName("surface_water")]
        public string SurfaceWater { get; set; } = string.Empty;

        [JsonPropertyName("population")]
        public string Population { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }
    }
}
