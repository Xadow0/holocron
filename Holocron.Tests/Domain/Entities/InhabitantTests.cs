using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Holocron.Domain.Entities;

namespace Holocron.Tests.Domain.Entities
{
    public class InhabitantTests
    {
        [Fact]
        public void Constructor_WithValidParameters_CreatesInhabitantCorrectly()
        {
            // Arrange
            string name = "Luke Skywalker";
            string species = "Human";
            string origin = "Tatooine";
            bool isSuspectedRebel = true;

            // Act
            var inhabitant = new Inhabitant(name, species, origin, isSuspectedRebel);

            // Assert
            Assert.Equal(name, inhabitant.Name);
            Assert.Equal(species, inhabitant.Species);
            Assert.Equal(origin, inhabitant.Origin);
            Assert.Equal(isSuspectedRebel, inhabitant.IsSuspectedRebel);
            Assert.NotEqual(Guid.Empty, inhabitant.Id);
            Assert.True((DateTime.UtcNow - inhabitant.CreatedAt).TotalSeconds < 5);
        }

        [Fact]
        public void Constructor_WithNullName_ThrowsArgumentNullException()
        {
            // Arrange
            string name = null;
            string species = "Human";
            string origin = "Tatooine";
            bool isSuspectedRebel = true;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Inhabitant(name, species, origin, isSuspectedRebel));

            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithNullSpecies_ThrowsArgumentNullException()
        {
            // Arrange
            string name = "Luke Skywalker";
            string species = null;
            string origin = "Tatooine";
            bool isSuspectedRebel = true;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Inhabitant(name, species, origin, isSuspectedRebel));

            Assert.Equal("species", exception.ParamName);
        }

        [Fact]
        public void Update_WithValidParameters_UpdatesInhabitantCorrectly()
        {
            // Arrange
            var inhabitant = new Inhabitant("Luke Skywalker", "Human", "Tatooine", true);

            string newName = "Leia Organa";
            string newSpecies = "Human";
            string newOrigin = "Alderaan";
            bool newIsSuspectedRebel = false;

            // Act
            inhabitant.Update(newName, newSpecies, newOrigin, newIsSuspectedRebel);

            // Assert
            Assert.Equal(newName, inhabitant.Name);
            Assert.Equal(newSpecies, inhabitant.Species);
            Assert.Equal(newOrigin, inhabitant.Origin);
            Assert.Equal(newIsSuspectedRebel, inhabitant.IsSuspectedRebel);
        }

        [Fact]
        public void Update_WithNullName_ThrowsArgumentNullException()
        {
            // Arrange
            var inhabitant = new Inhabitant("Luke Skywalker", "Human", "Tatooine", true);

            string newName = null;
            string newSpecies = "Human";
            string newOrigin = "Alderaan";
            bool newIsSuspectedRebel = false;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                inhabitant.Update(newName, newSpecies, newOrigin, newIsSuspectedRebel));

            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void Update_WithNullSpecies_ThrowsArgumentNullException()
        {
            // Arrange
            var inhabitant = new Inhabitant("Luke Skywalker", "Human", "Tatooine", true);

            string newName = "Leia Organa";
            string newSpecies = null;
            string newOrigin = "Alderaan";
            bool newIsSuspectedRebel = false;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                inhabitant.Update(newName, newSpecies, newOrigin, newIsSuspectedRebel));

            Assert.Equal("species", exception.ParamName);
        }
    }
}
