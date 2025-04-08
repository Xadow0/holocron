using Holocron.Application.DTOs.Inhabitants;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Application.Features.Inhabitants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Holocron.API.Controllers
{
    [ApiController]
    [Route("api/inhabitants")]
    public class InhabitantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InhabitantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInhabitantsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetInhabitantByIdQuery(id));
            if (result == null)
                return NotFound(new { Message = "Habitante no encontrado" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InhabitantCreateDto dto)
        {
            var result = await _mediator.Send(new CreateInhabitantCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InhabitantCreateDto dto)
        {
            var result = await _mediator.Send(new UpdateInhabitantCommand(id, dto));
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteInhabitantCommand(id));
            return Ok(new { Message = "Habitante eliminado exitosamente" });
        }

        [HttpGet("rebels")]
        public async Task<IActionResult> GetRebels()
        {
            var result = await _mediator.Send(new GetRebelInhabitantsQuery());
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _mediator.Send(new SearchInhabitantsQuery(query));
            return Ok(result);
        }
    }
}
