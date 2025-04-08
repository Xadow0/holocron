using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Queries;
using Holocron.Domain.Interfaces.Repositories;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Handlers
{
    public class GetInhabitantByIdQueryHandler : IRequestHandler<GetInhabitantByIdQuery, InhabitantReadDto?>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public GetInhabitantByIdQueryHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<InhabitantReadDto?> Handle(GetInhabitantByIdQuery request, CancellationToken cancellationToken)
        {
            var inhabitant = await _inhabitantRepository.GetByIdAsync(request.Id);
            return inhabitant == null ? null : new InhabitantReadDto
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