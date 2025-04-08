using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Queries
{
    public record GetRebelInhabitantsQuery() : IRequest<IEnumerable<InhabitantReadDto>>;
}
