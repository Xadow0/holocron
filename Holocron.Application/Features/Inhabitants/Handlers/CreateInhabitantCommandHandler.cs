using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Domain.Entities;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Application.DTOs.Inhabitants;

namespace Holocron.Application.Features.Inhabitants.Handlers
{
    public class CreateInhabitantCommandHandler : IRequestHandler<CreateInhabitantCommand, InhabitantReadDto>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public CreateInhabitantCommandHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<InhabitantReadDto> Handle(CreateInhabitantCommand request, CancellationToken cancellationToken)
        {
            var dto = request.InhabitantDto;
            var inhabitant = new Inhabitant(dto.Name, dto.Species, dto.Origin, dto.IsSuspectedRebel);

            await _inhabitantRepository.AddAsync(inhabitant);

            return new InhabitantReadDto
            {
                Id = inhabitant.Id,
                Name = inhabitant.Name,
                Species = inhabitant.Species,
                Origin = inhabitant.Origin,
                IsSuspectedRebel = inhabitant.IsSuspectedRebel,
                CreatedAt = inhabitant.CreatedAt
            };
        }
    }
}