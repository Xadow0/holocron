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
    public class GetRebelInhabitantsQueryHandler : IRequestHandler<GetRebelInhabitantsQuery, IEnumerable<InhabitantReadDto>>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public GetRebelInhabitantsQueryHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<IEnumerable<InhabitantReadDto>> Handle(GetRebelInhabitantsQuery request, CancellationToken cancellationToken)
        {
            var rebels = await _inhabitantRepository.GetRebelsAsync();
            return rebels.Select(r => new InhabitantReadDto
            {
                Id = r.Id,
                Name = r.Name,
                Species = r.Species,
                Origin = r.Origin,
                IsSuspectedRebel = r.IsSuspectedRebel,
                CreatedAt = r.CreatedAt
            });
        }
    }
}