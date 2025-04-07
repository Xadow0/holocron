using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Commands
{
    public class CreateInhabitantCommand : IRequest<InhabitantReadDto>
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string? Origin { get; set; }
        public bool IsSuspectedRebel { get; set; }
    }
}
