using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Holocron.API.Controllers
{
    [ApiController]
    [Route("api/inhabitants")]
    public class InhabitantsController : ControllerBase
    {
        private readonly IInhabitantService _inhabitantService;

        public InhabitantsController(IInhabitantService inhabitantService)
        {
            _inhabitantService = inhabitantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inhabitants = await _inhabitantService.GetAllInhabitantsAsync();
            return Ok(inhabitants);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var inhabitant = await _inhabitantService.GetInhabitantByIdAsync(id);
            if (inhabitant == null)
                return NotFound(new { Message = "Habitante no encontrado" });

            return Ok(inhabitant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InhabitantCreateDto dto)
        {
            await _inhabitantService.CreateInhabitantAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { Message = "Habitante creado exitosamente" });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InhabitantCreateDto dto)
        {
            await _inhabitantService.UpdateInhabitantAsync(id, dto);
            return Ok(new { Message = "Habitante actualizado exitosamente" });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _inhabitantService.DeleteInhabitantAsync(id);
            return Ok(new { Message = "Habitante eliminado exitosamente" });
        }

        [HttpGet("rebels")]
        public async Task<IActionResult> GetRebels()
        {
            var rebels = await _inhabitantService.GetRebelsAsync();
            return Ok(rebels);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var inhabitants = await _inhabitantService.SearchInhabitantsAsync(query);
            return Ok(inhabitants);
        }
    }
}
