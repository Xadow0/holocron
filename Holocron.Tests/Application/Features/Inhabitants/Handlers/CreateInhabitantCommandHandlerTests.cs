using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Application.Features.Inhabitants.Handlers;
using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Holocron.Tests.Application.Features.Inhabitants.Handlers
{
    public class CreateInhabitantCommandHandlerTests
    {
        private readonly Mock<IInhabitantRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateInhabitantCommandHandler _handler;

        public CreateInhabitantCommandHandlerTests()
        {
            _mockRepository = new Mock<IInhabitantRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateInhabitantCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_CreatesInhabitantAndReturnsDto()
        {
            // Arrange
            var command = new CreateInhabitantCommand
            {
                Name = "Han Solo",
                Species = "Human",
                Origin = null,
                IsSuspectedRebel = true
            };

            // Crear la entidad que será añadida por el repositorio
            var addedInhabitant = new Inhabitant(
                command.Name,
                command.Species,
                command.Origin,
                command.IsSuspectedRebel
            );

            var expectedDto = new InhabitantReadDto
            {
                Id = addedInhabitant.Id,
                Name = addedInhabitant.Name,
                Species = addedInhabitant.Species,
                Origin = addedInhabitant.Origin,
                IsSuspectedRebel = addedInhabitant.IsSuspectedRebel,
                CreatedAt = addedInhabitant.CreatedAt
            };

            // Mock para el repositorio: devuelve la entidad Inhabitant después de agregarla
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Inhabitant>()))
                .ReturnsAsync(addedInhabitant);

            // Mock para el Mapper: mapea la entidad Inhabitant a un InhabitantReadDto
            _mockMapper.Setup(m => m.Map<InhabitantReadDto>(It.IsAny<Inhabitant>()))
                .Returns(expectedDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto, result);

            // Verifica que AddAsync fue llamado correctamente
            _mockRepository.Verify(r => r.AddAsync(It.Is<Inhabitant>(i =>
                i.Name == command.Name &&
                i.Species == command.Species &&
                i.Origin == command.Origin &&
                i.IsSuspectedRebel == command.IsSuspectedRebel)),
                Times.Once);

            // Verifica que Map fue llamado correctamente
            _mockMapper.Verify(m => m.Map<InhabitantReadDto>(It.IsAny<Inhabitant>()), Times.Once);
        }



        [Fact]
        public async Task Handle_WithNullProperties_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new CreateInhabitantCommand
            {
                Name = null,
                Species = "Human",
                Origin = "Corellia",
                IsSuspectedRebel = true
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _handler.Handle(command, CancellationToken.None));

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Inhabitant>()), Times.Never);
        }
    }
}
