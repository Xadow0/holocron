using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.Interfaces.Services;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories;
using Holocron.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Holocron.Infrastructure.Configurations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IInhabitantRepository, InhabitantRepository>();
            services.AddScoped<IInhabitantService, InhabitantService>();
            return services;
        }
    }
}
