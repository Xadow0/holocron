using AutoMapper;
using MediatR;
using Holocron.Application.DTOs.Planets;
using Holocron.Application.Features.Planets.Queries;
using Holocron.Domain.Interfaces.Repositories;

namespace Holocron.Application.Features.Planets.Handlers
{
    public class GetAllPlanetsQueryHandler : IRequestHandler<GetAllPlanetsQuery, IEnumerable<PlanetReadDto>>
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IMapper _mapper;

        public GetAllPlanetsQueryHandler(IPlanetRepository planetRepository, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanetReadDto>> Handle(GetAllPlanetsQuery request, CancellationToken cancellationToken)
        {
            var planets = await _planetRepository.GetAllPlanetsAsync();
            return _mapper.Map<IEnumerable<PlanetReadDto>>(planets);
        }
    }
}
