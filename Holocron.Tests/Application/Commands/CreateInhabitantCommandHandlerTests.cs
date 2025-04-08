using System;
using System.Threading;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Application.Features.Inhabitants.Handlers;
using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;

namespace Holocron.Application.Tests.Features.Inhabitants.Handlers
{
    public class CreateInhabitantCommandHandlerTests
    {
        private readonly Mock<IInhabitantRepository> _mockRepository;
        private readonly CreateInhabitantCommandHandler _handler;

        public CreateInhabitantCommandHandlerTests()
        {
            _mockRepository = new Mock<IInhabitantRepository>();
            _handler = new CreateInhabitantCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldAddInhabitantToRepository()
        {
            // Arrange
            var createDto = new InhabitantCreateDto
            {
                Name = "Luke Skywalker",
                Species = "Human",
                Origin = "Tatooine",
                IsSuspectedRebel = true
            };

            var command = new CreateInhabitantCommand(createDto);

            _mockRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Inhabitant>()))
                .ReturnsAsync((Inhabitant inhabitant) => inhabitant)
                .Callback<Inhabitant>(inhabitant =>
                {
                    // Simulate database setting the Id
                    inhabitant.GetType()
                        .GetProperty("Id")
                        .SetValue(inhabitant, Guid.NewGuid());
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.Is<Inhabitant>(i =>
                i.Name == createDto.Name &&
                i.Species == createDto.Species &&
                i.Origin == createDto.Origin &&
                i.IsSuspectedRebel == createDto.IsSuspectedRebel
            )), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Species, result.Species);
            Assert.Equal(createDto.Origin, result.Origin);
            Assert.Equal(createDto.IsSuspectedRebel, result.IsSuspectedRebel);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.NotEqual(default, result.CreatedAt);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnCorrectDto()
        {
            // Arrange
            var createDto = new InhabitantCreateDto
            {
                Name = "Darth Vader",
                Species = "Human",
                Origin = "Tatooine",
                IsSuspectedRebel = false
            };

            var command = new CreateInhabitantCommand(createDto);
            var expectedId = Guid.NewGuid();
            var expectedCreatedAt = DateTime.UtcNow;

            _mockRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Inhabitant>()))
                .ReturnsAsync((Inhabitant inhabitant) => inhabitant)
                .Callback<Inhabitant>(inhabitant =>
                {
                    // Simulate database setting values
                    var type = inhabitant.GetType();
                    type.GetProperty("Id").SetValue(inhabitant, expectedId);
                    type.GetProperty("CreatedAt").SetValue(inhabitant, expectedCreatedAt);
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<InhabitantReadDto>(result);
            Assert.Equal(expectedId, result.Id);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Species, result.Species);
            Assert.Equal(createDto.Origin, result.Origin);
            Assert.Equal(createDto.IsSuspectedRebel, result.IsSuspectedRebel);
            Assert.Equal(expectedCreatedAt, result.CreatedAt);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var inhabitant = new InhabitantCreateDto
            {
                Name = "Test Inhabitant",
                Species = "Test Species",
                Origin = "Test Origin",
                IsSuspectedRebel = true
            };
            var command = new CreateInhabitantCommand(inhabitant);

            _mockRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Inhabitant>()))
                .ThrowsAsync(new Exception("Database connection error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
