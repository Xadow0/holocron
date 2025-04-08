using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Domain.Interfaces.Repositories;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Handlers
{
    public class UpdateInhabitantCommandHandler : IRequestHandler<UpdateInhabitantCommand, InhabitantReadDto>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public UpdateInhabitantCommandHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<InhabitantReadDto> Handle(UpdateInhabitantCommand request, CancellationToken cancellationToken)
        {
            var inhabitant = await _inhabitantRepository.GetByIdAsync(request.Id);
            if (inhabitant == null)
                throw new KeyNotFoundException($"Habitante con ID {request.Id} no encontrado");

            var dto = request.InhabitantDto;
            inhabitant.Update(dto.Name, dto.Species, dto.Origin, dto.IsSuspectedRebel);

            await _inhabitantRepository.UpdateAsync(inhabitant);

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
