using AutoMapper;
using MediatR;
using Holocron.Application.DTOs.Planets;
using Holocron.Application.Features.Planets.Queries;
using Holocron.Domain.Interfaces.Repositories;

namespace Holocron.Application.Features.Planets.Handlers
{
    public class GetPlanetByIdQueryHandler : IRequestHandler<GetPlanetByIdQuery, PlanetReadDto>
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IMapper _mapper;

        public GetPlanetByIdQueryHandler(IPlanetRepository planetRepository, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _mapper = mapper;
        }

        public async Task<PlanetReadDto> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken)
        {
            var planet = await _planetRepository.GetPlanetByIdAsync(request.Id);

            if (planet == null)
                throw new KeyNotFoundException($"Planet with ID {request.Id} not found");

            return _mapper.Map<PlanetReadDto>(planet);
        }
    }
}