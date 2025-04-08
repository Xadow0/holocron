using System;
using System.Threading;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Handlers;
using Holocron.Application.Features.Inhabitants.Queries;
using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Holocron.Application.Tests.Features.Inhabitants.Handlers
{
    public class GetInhabitantByIdQueryHandlerTests
    {
        private readonly Mock<IInhabitantRepository> _mockRepository;
        private readonly GetInhabitantByIdQueryHandler _handler;

        public GetInhabitantByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IInhabitantRepository>();
            _handler = new GetInhabitantByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ShouldReturnInhabitant()
        {
            // Arrange
            var inhabitantId = Guid.NewGuid();
            var inhabitant = new Inhabitant("Han Solo", "Human", "Corellia", true);

            // Setting properties using reflection since they might be private setters
            var type = inhabitant.GetType();
            type.GetProperty("Id").SetValue(inhabitant, inhabitantId);
            var createdAt = DateTime.UtcNow.AddDays(-1);
            type.GetProperty("CreatedAt").SetValue(inhabitant, createdAt);

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(inhabitantId))
                .ReturnsAsync(inhabitant);

            var query = new GetInhabitantByIdQuery(inhabitantId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inhabitantId, result.Id);
            Assert.Equal(inhabitant.Name, result.Name);
            Assert.Equal(inhabitant.Species, result.Species);
            Assert.Equal(inhabitant.Origin, result.Origin);
            Assert.Equal(inhabitant.IsSuspectedRebel, result.IsSuspectedRebel);
            Assert.Equal(createdAt, result.CreatedAt);

            _mockRepository.Verify(repo => repo.GetByIdAsync(inhabitantId), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(nonExistingId))
                .ReturnsAsync((Inhabitant)null);

            var query = new GetInhabitantByIdQuery(nonExistingId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.GetByIdAsync(nonExistingId), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var inhabitantId = Guid.NewGuid();

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(inhabitantId))
                .ThrowsAsync(new Exception("Database connection error"));

            var query = new GetInhabitantByIdQuery (inhabitantId);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _handler.Handle(query, CancellationToken.None));
        }
    }
}