using Holocron.Application.DTOs.Inhabitants;
using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holocron.Tests.Application.Services
{
    public class InhabitantServiceTests
    {
        private readonly Mock<IInhabitantRepository> _mockRepository;
        private readonly InhabitantService _service;

        public InhabitantServiceTests()
        {
            _mockRepository = new Mock<IInhabitantRepository>();
            _service = new InhabitantService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllInhabitantsAsync_ReturnsAllInhabitants()
        {
            // Arrange
            var inhabitants = new List<Inhabitant>
            {
                new Inhabitant("Han Solo", "Human", "Corellia", true),
                new Inhabitant("Chewbacca", "Wookiee", "Kashyyyk", true)
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(inhabitants);

            // Act
            var result = await _service.GetAllInhabitantsAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal(inhabitants[0].Name, resultList[0].Name);
            Assert.Equal(inhabitants[0].Species, resultList[0].Species);
            Assert.Equal(inhabitants[0].Origin, resultList[0].Origin);
            Assert.Equal(inhabitants[0].IsSuspectedRebel, resultList[0].IsSuspectedRebel);
            Assert.Equal(inhabitants[0].Id, resultList[0].Id);
            Assert.Equal(inhabitants[0].CreatedAt, resultList[0].CreatedAt);

            Assert.Equal(inhabitants[1].Name, resultList[1].Name);
            Assert.Equal(inhabitants[1].Species, resultList[1].Species);

            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetInhabitantByIdAsync_WithExistingId_ReturnsInhabitant()
        {
            // Arrange
            var id = Guid.NewGuid();
            var inhabitant = new Inhabitant("Han Solo", "Human", "Corellia", true);

            // Use reflection to set Id since it's a private setter
            typeof(Inhabitant).GetProperty("Id").SetValue(inhabitant, id);

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(inhabitant);

            // Act
            var result = await _service.GetInhabitantByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inhabitant.Name, result.Name);
            Assert.Equal(inhabitant.Species, result.Species);
            Assert.Equal(inhabitant.Origin, result.Origin);
            Assert.Equal(inhabitant.IsSuspectedRebel, result.IsSuspectedRebel);
            Assert.Equal(inhabitant.Id, result.Id);
            Assert.Equal(inhabitant.CreatedAt, result.CreatedAt);

            _mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetInhabitantByIdAsync_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Inhabitant)null);

            // Act
            var result = await _service.GetInhabitantByIdAsync(id);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateInhabitantAsync_CreatesAndSavesInhabitant()
        {
            // Arrange
            var dto = new InhabitantCreateDto
            {
                Name = "Han Solo",
                Species = "Human",
                Origin = "Corellia",
                IsSuspectedRebel = true
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Inhabitant>()))
                .ReturnsAsync((Inhabitant i) => i);

            // Act
            await _service.CreateInhabitantAsync(dto);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.Is<Inhabitant>(i =>
                i.Name == dto.Name &&
                i.Species == dto.Species &&
                i.Origin == dto.Origin &&
                i.IsSuspectedRebel == dto.IsSuspectedRebel)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateInhabitantAsync_WithExistingId_UpdatesInhabitant()
        {
            // Arrange
            var id = Guid.NewGuid();
            var inhabitant = new Inhabitant("Old Name", "Old Species", "Old Origin", false);

            // Use reflection to set Id since it's a private setter
            typeof(Inhabitant).GetProperty("Id").SetValue(inhabitant, id);

            var dto = new InhabitantCreateDto
            {
                Name = "New Name",
                Species = "New Species",
                Origin = "New Origin",
                IsSuspectedRebel = true
            };

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(inhabitant);

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Inhabitant>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateInhabitantAsync(id, dto);

            // Assert
            _mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Inhabitant>(i =>
                i.Id == id &&
                i.Name == dto.Name &&
                i.Species == dto.Species &&
                i.Origin == dto.Origin &&
                i.IsSuspectedRebel == dto.IsSuspectedRebel)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateInhabitantAsync_WithNonExistingId_DoesNothing()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new InhabitantCreateDto
            {
                Name = "New Name",
                Species = "New Species",
                Origin = "New Origin",
                IsSuspectedRebel = true
            };

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Inhabitant)null);

            // Act
            await _service.UpdateInhabitantAsync(id, dto);

            // Assert
            _mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Inhabitant>()), Times.Never);
        }

        [Fact]
        public async Task DeleteInhabitantAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRepository.Setup(r => r.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteInhabitantAsync(id);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetRebelsAsync_ReturnsOnlySuspectedRebels()
        {
            // Arrange
            var rebels = new List<Inhabitant>
            {
                new Inhabitant("Han Solo", "Human", "Corellia", true),
                new Inhabitant("Leia Organa", "Human", "Alderaan", true)
            };

            _mockRepository.Setup(r => r.GetRebelsAsync())
                .ReturnsAsync(rebels);

            // Act
            var result = await _service.GetRebelsAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal(rebels[0].Name, resultList[0].Name);
            Assert.Equal(rebels[1].Name, resultList[1].Name);
            Assert.True(resultList.All(r => r.IsSuspectedRebel));

            _mockRepository.Verify(r => r.GetRebelsAsync(), Times.Once);
        }

        [Fact]
        public async Task SearchInhabitantsAsync_ReturnsMatchingInhabitants()
        {
            // Arrange
            string searchQuery = "Solo";
            var matchingInhabitants = new List<Inhabitant>
            {
                new Inhabitant("Han Solo", "Human", "Corellia", true)
            };

            _mockRepository.Setup(r => r.SearchInhabitantsAsync(searchQuery))
                .ReturnsAsync(matchingInhabitants);

            // Act
            var result = await _service.SearchInhabitantsAsync(searchQuery);

            // Assert
            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal(matchingInhabitants[0].Name, resultList[0].Name);

            _mockRepository.Verify(r => r.SearchInhabitantsAsync(searchQuery), Times.Once);
        }
    }
}
