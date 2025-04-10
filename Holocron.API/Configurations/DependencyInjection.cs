using Holocron.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Holocron.Application.Features.Planets.Handlers;
using AutoMapper;
using Holocron.Application.Mappings;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories;

namespace Holocron.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //MediatR
            var applicationAssembly = typeof(GetAllPlanetsQueryHandler).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            // Registrar AutoMapper - importante para resolver IMapper
            services.AddAutoMapper(cfg => {
            }, typeof(PlanetMappingProfile).Assembly); // Asume que PlanetMappingProfile está en el mismo assembly que los perfiles

            services.AddHttpClient<SwapiService>();
            services.AddScoped<IPlanetRepository, PlanetRepository>();
            services.AddScoped<IInhabitantRepository, InhabitantRepository>();
            services.AddControllers();
            return services;
        }
    }
}