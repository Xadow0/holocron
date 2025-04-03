FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el directorio Holocron al contenedor
COPY Holocron/ /src/Holocron/

# Restaurar las dependencias de los proyectos
RUN dotnet restore "/src/Holocron/Holocron.API/Holocron.API.csproj"

# Construir el proyecto Holocron.API
WORKDIR "/src/Holocron/Holocron.API"
RUN dotnet build "Holocron.API.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "Holocron.API.csproj" -c Release -o /app/publish

# Fase final para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Holocron.API.dll"]