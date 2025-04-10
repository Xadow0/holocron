using MediatR;
using Microsoft.AspNetCore.Mvc;
using Holocron.Application.DTOs.Planets;
using Holocron.Application.Features.Planets.Queries;

namespace Holocron.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanetReadDto>>> GetAllPlanets()
        {
            var planets = await _mediator.Send(new GetAllPlanetsQuery());
            return Ok(planets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanetReadDto>> GetPlanetById(int id)
        {
            try
            {
                var planet = await _mediator.Send(new GetPlanetByIdQuery(id));
                return Ok(planet);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PlanetReadDto>>> SearchPlanets([FromQuery] string term)
        {
            var planets = await _mediator.Send(new SearchPlanetsQuery(term));
            return Ok(planets);
        }
    }
}