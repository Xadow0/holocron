using MediatR;
using Holocron.Application.DTOs.Planets;

namespace Holocron.Application.Features.Planets.Queries
{
    public class GetAllPlanetsQuery : IRequest<IEnumerable<PlanetReadDto>>
    {
    }
}
