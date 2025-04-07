# Fase base para la ejecución de la app
# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de los proyectos
COPY ["Holocron.API/Holocron.API.csproj", "Holocron.API/"]
COPY ["Holocron.Application/Holocron.Application.csproj", "Holocron.Application/"]
COPY ["Holocron.Domain/Holocron.Domain.csproj", "Holocron.Domain/"]
COPY ["Holocron.Infrastructure/Holocron.Infrastructure.csproj", "Holocron.Infrastructure/"]

# Restaurar las dependencias
RUN dotnet restore "Holocron.API/Holocron.API.csproj"

# Copiar el resto de los archivos
COPY . .

# Construir el proyecto
WORKDIR "/src/Holocron.API"
RUN dotnet build "Holocron.API.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "Holocron.API.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Holocron.API.dll"]


