using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holocron.Domain.DTOs.External
{
    public class PlanetDto
    {
        public string Name { get; set; } = string.Empty;
        public string Climate { get; set; } = string.Empty;
        public string Terrain { get; set; } = string.Empty;
        public string Population { get; set; } = string.Empty;
    }
}
