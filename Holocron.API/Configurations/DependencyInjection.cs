using Holocron.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Holocron.Application.Features.Planets.Handlers;
using AutoMapper;
using Holocron.Application.Mappings;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories; // Asegúrate de tener el namespace correcto

namespace Holocron.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Registrar MediatR
            var applicationAssembly = typeof(GetAllPlanetsQueryHandler).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            // Registrar AutoMapper - importante para resolver IMapper
            services.AddAutoMapper(cfg => {
                // Aquí puedes añadir configuraciones específicas si es necesario
            }, typeof(PlanetMappingProfile).Assembly); // Asume que PlanetMappingProfile está en el mismo assembly que los perfiles

            // O simplemente:
            // services.AddAutoMapper(typeof(PlanetMappingProfile).Assembly);

            services.AddHttpClient<SwapiService>();
            services.AddScoped<IPlanetRepository, PlanetRepository>();
            services.AddScoped<IInhabitantRepository, InhabitantRepository>();
            services.AddControllers();
            return services;
        }
    }
}