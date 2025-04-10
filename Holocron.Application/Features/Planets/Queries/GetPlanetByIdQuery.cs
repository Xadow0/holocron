using MediatR;
using Holocron.Application.DTOs.Planets;

namespace Holocron.Application.Features.Planets.Queries
{
    public class GetPlanetByIdQuery : IRequest<PlanetReadDto>
    {
        public int Id { get; set; }

        public GetPlanetByIdQuery(int id)
        {
            Id = id;
        }
    }
}
