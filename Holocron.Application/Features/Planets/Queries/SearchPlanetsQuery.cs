using MediatR;
using Holocron.Application.DTOs.Planets;

namespace Holocron.Application.Features.Planets.Queries
{
    public class SearchPlanetsQuery : IRequest<IEnumerable<PlanetReadDto>>
    {
        public string SearchTerm { get; set; }

        public SearchPlanetsQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
