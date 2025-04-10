using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holocron.Domain.Entities
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Climate { get; set; } = string.Empty;
        public string Terrain { get; set; } = string.Empty;
        public string Population { get; set; } = string.Empty;
        public string Diameter { get; set; } = string.Empty;
        public string Gravity { get; set; } = string.Empty;
        public string OrbitalPeriod { get; set; } = string.Empty;
        public string RotationPeriod { get; set; } = string.Empty;
        public string SurfaceWater { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}