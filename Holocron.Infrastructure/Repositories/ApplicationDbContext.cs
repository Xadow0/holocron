using Holocron.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Holocron.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Inhabitant> Inhabitants { get; set; }
    }
}
