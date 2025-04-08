using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holocron.Domain.Entities;
using Holocron.Infrastructure.Persistence;
using Holocron.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Holocron.Infrastructure.Tests.Repositories
{
    public class InhabitantRepositoryTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        private async Task<ApplicationDbContext> SeedData(params Inhabitant[] inhabitants)
        {
            var context = CreateContext();
            await context.Inhabitants.AddRangeAsync(inhabitants);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllInhabitants()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Darth Vader", "Human", "Tatooine", false),
                new Inhabitant("Princess Leia", "Human", "Alderaan", true)
            };

            var context = await SeedData(inhabitants.ToArray());
            var repository = new InhabitantRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(3, resultList.Count);
            Assert.Contains(resultList, i => i.Name == "Luke Skywalker");
            Assert.Contains(resultList, i => i.Name == "Darth Vader");
            Assert.Contains(resultList, i => i.Name == "Princess Leia");
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldReturnInhabitant()
        {
            // Arrange
            var inhabitant = new Inhabitant("Han Solo", "Human", "Corellia", true);
            var context = await SeedData(inhabitant);
            var repository = new InhabitantRepository(context);

            // Get the Id from the seeded data
            var seededInhabitant = await context.Inhabitants.FirstAsync();
            var id = seededInhabitant.Id;

            // Act
            var result = await repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Han Solo", result.Name);
            Assert.Equal("Human", result.Species);
            Assert.Equal("Corellia", result.Origin);
            Assert.True(result.IsSuspectedRebel);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var context = CreateContext();
            var repository = new InhabitantRepository(context);
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await repository.GetByIdAsync(nonExistingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ValidInhabitant_ShouldAddAndReturnInhabitant()
        {
            // Arrange
            var context = CreateContext();
            var repository = new InhabitantRepository(context);
            var inhabitant = new Inhabitant("Chewbacca", "Wookiee", "Kashyyyk", true);

            // Act
            var result = await repository.AddAsync(inhabitant);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inhabitant.Id, result.Id);
            Assert.Equal("Chewbacca", result.Name);

            // Verify it's in the database
            var fromDb = await context.Inhabitants.FindAsync(result.Id);
            Assert.NotNull(fromDb);
            Assert.Equal("Chewbacca", fromDb.Name);
            Assert.Equal("Wookiee", fromDb.Species);
        }

        [Fact]
        public async Task UpdateAsync_ExistingInhabitant_ShouldUpdateInhabitant()
        {
            // Arrange
            var inhabitant = new Inhabitant("Obi-Wan Kenobi", "Human", "Stewjon", false);
            var context = await SeedData(inhabitant);
            var repository = new InhabitantRepository(context);

            // Get the seeded inhabitant to update
            var toUpdate = await context.Inhabitants.FirstAsync();

            // Update properties
            toUpdate.GetType().GetProperty("Name").SetValue(toUpdate, "Ben Kenobi");
            toUpdate.GetType().GetProperty("IsSuspectedRebel").SetValue(toUpdate, true);

            // Act
            await repository.UpdateAsync(toUpdate);

            // Assert
            var updated = await context.Inhabitants.FindAsync(toUpdate.Id);
            Assert.NotNull(updated);
            Assert.Equal("Ben Kenobi", updated.Name);
            Assert.Equal("Human", updated.Species);
            Assert.Equal("Stewjon", updated.Origin);
            Assert.True(updated.IsSuspectedRebel);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_ShouldRemoveInhabitant()
        {
            // Arrange
            var inhabitant = new Inhabitant("Lando Calrissian", "Human", "Socorro", true);
            var context = await SeedData(inhabitant);
            var repository = new InhabitantRepository(context);

            var seeded = await context.Inhabitants.FirstAsync();
            var id = seeded.Id;

            // Act
            await repository.DeleteAsync(id);

            // Assert
            var result = await context.Inhabitants.FindAsync(id);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ShouldNotThrowException()
        {
            // Arrange
            var context = CreateContext();
            var repository = new InhabitantRepository(context);
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            await repository.DeleteAsync(nonExistingId); // Should not throw
        }

        [Fact]
        public async Task GetRebelsAsync_ShouldReturnOnlyRebels()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Darth Vader", "Human", "Tatooine", false),
                new Inhabitant("Princess Leia", "Human", "Alderaan", true),
                new Inhabitant("Grand Moff Tarkin", "Human", "Eriadu", false)
            };

            var context = await SeedData(inhabitants.ToArray());
            var repository = new InhabitantRepository(context);

            // Act
            var rebels = await repository.GetRebelsAsync();

            // Assert
            var rebelList = rebels.ToList();
            Assert.Equal(2, rebelList.Count);
            Assert.Contains(rebelList, i => i.Name == "Luke Skywalker");
            Assert.Contains(rebelList, i => i.Name == "Princess Leia");
            Assert.DoesNotContain(rebelList, i => i.Name == "Darth Vader");
            Assert.DoesNotContain(rebelList, i => i.Name == "Grand Moff Tarkin");
        }

        [Fact]
        public async Task SearchInhabitantsAsync_SearchByName_ShouldReturnMatchingInhabitants()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Leia Organa", "Human", "Alderaan", true),
                new Inhabitant("Han Solo", "Human", "Corellia", true),
                new Inhabitant("Chewbacca", "Wookiee", "Kashyyyk", true)
            };

            var context = await SeedData(inhabitants.ToArray());
            var repository = new InhabitantRepository(context);

            // Act
            var results = await repository.SearchInhabitantsAsync("Lu");

            // Assert
            var resultList = results.ToList();
            Assert.Single(resultList);
            Assert.Equal("Luke Skywalker", resultList[0].Name);
        }

        [Fact]
        public async Task SearchInhabitantsAsync_SearchBySpecies_ShouldReturnMatchingInhabitants()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Leia Organa", "Human", "Alderaan", true),
                new Inhabitant("Chewbacca", "Wookiee", "Kashyyyk", true),
                new Inhabitant("Jabba", "Hutt", "Nal Hutta", false)
            };

            var context = await SeedData(inhabitants.ToArray());
            var repository = new InhabitantRepository(context);

            // Act
            var results = await repository.SearchInhabitantsAsync("Wook");

            // Assert
            var resultList = results.ToList();
            Assert.Single(resultList);
            Assert.Equal("Chewbacca", resultList[0].Name);
            Assert.Equal("Wookiee", resultList[0].Species);
        }

        [Fact]
        public async Task SearchInhabitantsAsync_NoMatches_ShouldReturnEmptyCollection()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Luke Skywalker", "Human", "Tatooine", true),
                new Inhabitant("Leia Organa", "Human", "Alderaan", true)
            };

            var context = await SeedData(inhabitants.ToArray());
            var repository = new InhabitantRepository(context);

            // Act
            var results = await repository.SearchInhabitantsAsync("Sith");

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Constructor_NullContext_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new InhabitantRepository(null));
            Assert.Equal("context", exception.ParamName);
        }
    }
}