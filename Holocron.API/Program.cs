using Holocron.API.Middlewares;
using Holocron.Application.Configurations;
using Holocron.Infrastructure;
using Holocron.Infrastructure.Configurations;
using Holocron.Infrastructure.Persistence;
using Holocron.Application.Interfaces.Services;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories;
using Holocron.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Identity;
using Holocron.Infrastructure.SeedData;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure services to use Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Endpoint=https://holocron-appconfig.azconfig.io")
           .Select(KeyFilter.Any) // Select All Keys
           .Select(LabelFilter.Null)
           .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential())); // Configure Key Vault using DefaultAzureCredential
});

//Insights telemetry configuration
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:Telemetry"];
});

// Database connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["SqlConnectionString"], sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    )
);

// CORS configuration with Azure
var corsOrigins = builder.Configuration["Frontend:CorsOrigins"]?.Split(",") ?? new string[] { };

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myCorsPolicy", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Holocron API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Initialize(scope.ServiceProvider);
}

// Middleware and API configuration
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holocron API v1"));
}

app.UseHttpsRedirection();
app.UseCors("_myCorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();

