FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Holocron.API/Holocron.API.csproj Holocron.API/
COPY Holocron.Application/Holocron.Application.csproj Holocron.Application/
COPY Holocron.Domain/Holocron.Domain.csproj Holocron.Domain/
COPY Holocron.Infrastructure/Holocron.Infrastructure.csproj Holocron.Infrastructure/

COPY . .
WORKDIR /app/Holocron.API
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
CMD ["dotnet", "Holocron.API.dll"]
EXPOSE 5149
