using AutoMapper;
using Holocron.Application.DTOs.Planets;
using Holocron.Domain.Entities;

namespace Holocron.Application.Mappings
{
    public class PlanetMappingProfile : Profile
    {
        public PlanetMappingProfile()
        {
            CreateMap<Planet, PlanetReadDto>();
        }
    }
}
