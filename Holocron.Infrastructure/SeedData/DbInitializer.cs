using Holocron.Domain.Entities;
using Holocron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Holocron.Infrastructure.SeedData
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbInitializer>>();

                try
                {
                    logger.LogInformation("Applying migrations...");
                    context.Database.Migrate();

                    if (!context.Inhabitants.Any())
                    {
                        logger.LogInformation("Seeding initial data...");
                        var inhabitants = GetPreconfiguredInhabitants();
                        context.Inhabitants.AddRange(inhabitants);
                        context.SaveChanges();
                        logger.LogInformation("Database has been seeded.");
                    }
                    else
                    {
                        logger.LogInformation("Database already contains data. Skipping seeding.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while initializing the database.");
                }
            }
        }

        private static List<Inhabitant> GetPreconfiguredInhabitants()
        {
            return new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Darth Vader", "Human", "Tatooine", false),
                new Inhabitant("Yoda", "Unknown", "Dagobah", true),
                new Inhabitant("Leia Organa", "Human", "Alderaan", true),
                new Inhabitant("Han Solo", "Human", "Corellia", true)
            };
        }
    }
}
