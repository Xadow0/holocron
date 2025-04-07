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
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);

var appConfigEndpoint = Environment.GetEnvironmentVariable("AZURE_APPCONFIG_ENDPOINT");
// Configure services to use Azure App Configuration
if (!string.IsNullOrWhiteSpace(appConfigEndpoint))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
               .Select(KeyFilter.Any)
               .Select(LabelFilter.Null)
               .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()));
    });
}
else
{
    throw new InvalidOperationException("AZURE_APPCONFIG_ENDPOINT value is missing.");
}

//Insights
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

