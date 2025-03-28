using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holocron.Domain.Entities
{
    public class Inhabitant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Species { get; private set; }
        public string Origin { get; private set; }
        public bool IsSuspectedRebel { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Private constructor for EF Core
        private Inhabitant() { }

        public Inhabitant(
            string name,
            string species,
            string origin,
            bool isSuspectedRebel)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Species = species ?? throw new ArgumentNullException(nameof(species));
            Origin = origin;
            IsSuspectedRebel = isSuspectedRebel;
            CreatedAt = DateTime.UtcNow;
        }

        // Method to update inhabitant details
        public void Update(
            string name,
            string species,
            string origin,
            bool isSuspectedRebel)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Species = species ?? throw new ArgumentNullException(nameof(species));
            Origin = origin;
            IsSuspectedRebel = isSuspectedRebel;
        }
    }
}
