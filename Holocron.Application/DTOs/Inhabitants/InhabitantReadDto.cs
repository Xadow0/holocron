using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holocron.Application.DTOs.Inhabitants
{
    public class InhabitantReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string? Origin { get; set; }
        public bool IsSuspectedRebel { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
