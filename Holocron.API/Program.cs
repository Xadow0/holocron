using Holocron.API.Middlewares;
using Holocron.Application.Configurations;
using Holocron.Infrastructure;
using Microsoft.OpenApi.Models;
using Holocron.Infrastructure.Configurations;
using Holocron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Holocron.Application.Interfaces.Services;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Repositories;
using Holocron.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
));

builder.Services.AddScoped<IInhabitantRepository, InhabitantRepository>();
builder.Services.AddScoped<IInhabitantService, InhabitantService>();

var corsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Frontend URL
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Service Configuration
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Holocron API", Version = "v1" });
});


var app = builder.Build();

// Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holocron API v1"));
}

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
