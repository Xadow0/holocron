using Holocron.API.Middlewares;
using Holocron.Application.Configurations;
using Holocron.Infrastructure;
using Holocron.Infrastructure.Persistence;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Identity;
using Holocron.Infrastructure.SeedData;
using Microsoft.OpenApi.Models;
using System.Net.WebSockets;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
var appConfigEndpoint = builder.Configuration["AZURE_APPCONFIG_ENDPOINT"];

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
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
}

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["AZURE_INSIGHT_URL"];
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
builder.Services.AddApiServices();
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

