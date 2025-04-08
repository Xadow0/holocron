using Holocron.Application.DTOs.Inhabitants;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Commands
{
    public record CreateInhabitantCommand(InhabitantCreateDto InhabitantDto) : IRequest<InhabitantReadDto>;
}