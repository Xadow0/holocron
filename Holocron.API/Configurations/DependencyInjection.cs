using System.Reflection;
using Holocron.Domain.Interfaces.Services;
using Holocron.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Holocron.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient<IPlanetService, PlanetService>(client =>
            {
                client.BaseAddress = new Uri("https://swapi.dev/api/");
            });
            return services;
        }
    }
}