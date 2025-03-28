using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Domain.Entities;
using Holocron.Application.Features.Inhabitants.Commands;
using Holocron.Application.DTOs.Inhabitants;

namespace Holocron.Application.Features.Inhabitants.Handlers
{
    public class CreateInhabitantCommandHandler
        : IRequestHandler<CreateInhabitantCommand, InhabitantReadDto>
    {
        private readonly IInhabitantRepository _repository;
        private readonly IMapper _mapper;

        public CreateInhabitantCommandHandler(
            IInhabitantRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InhabitantReadDto> Handle(
            CreateInhabitantCommand request,
            CancellationToken cancellationToken)
        {
            var inhabitant = new Inhabitant(
                request.Name,
                request.Species,
                request.Origin,
                request.IsSuspectedRebel
            );

            await _repository.AddAsync(inhabitant);

            return _mapper.Map<InhabitantReadDto>(inhabitant);
        }
    }
}
