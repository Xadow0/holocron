using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Commands
{
    public record DeleteInhabitantCommand(Guid Id) : IRequest;
}
