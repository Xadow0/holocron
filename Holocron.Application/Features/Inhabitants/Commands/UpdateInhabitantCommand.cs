using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Commands
{
    public record UpdateInhabitantCommand(Guid Id, InhabitantCreateDto InhabitantDto) : IRequest<InhabitantReadDto>;
}