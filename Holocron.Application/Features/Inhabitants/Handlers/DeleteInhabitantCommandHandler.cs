using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Domain.Interfaces.Repositories;
using MediatR;

namespace Holocron.Application.Features.Inhabitants.Handlers
{
    public class DeleteInhabitantCommandHandler : IRequestHandler<DeleteInhabitantCommand>
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public DeleteInhabitantCommandHandler(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task Handle(DeleteInhabitantCommand request, CancellationToken cancellationToken)
        {
            await _inhabitantRepository.DeleteAsync(request.Id);
        }
    }
}
