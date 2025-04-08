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
    public class GetAllInhabitantsQueryHandler : IRequestHandler<GetAllInhabitantsQuery, IEnumerable<InhabitantReadDto>>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public GetAllInhabitantsQueryHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<IEnumerable<InhabitantReadDto>> Handle(GetAllInhabitantsQuery request, CancellationToken cancellationToken)
        {
            var inhabitants = await _inhabitantRepository.GetAllAsync();
            return inhabitants.Select(i => new InhabitantReadDto
            {
                Id = i.Id,
                Name = i.Name,
                Species = i.Species,
                Origin = i.Origin,
                IsSuspectedRebel = i.IsSuspectedRebel,
                CreatedAt = i.CreatedAt
            });
        }
    }
}