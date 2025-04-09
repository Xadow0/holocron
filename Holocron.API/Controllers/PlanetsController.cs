using Holocron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Holocron.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetService _planetService;

        public PlanetsController(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanets()
        {
            var planets = await _planetService.GetAllPlanetsAsync();
            return Ok(planets);
        }
    }
}
