FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Holocron.API/Holocron.API.csproj", "Holocron.API/"]
COPY ["Holocron.Application/Holocron.Application.csproj", "Holocron.Application/"]
COPY ["Holocron.Domain/Holocron.Domain.csproj", "Holocron.Domain/"]
COPY ["Holocron.Infrastructure/Holocron.Infrastructure.csproj", "Holocron.Infrastructure/"]

RUN dotnet restore "Holocron.API/Holocron.API.csproj"

COPY . .
WORKDIR "/src/Holocron.API"
RUN dotnet build "Holocron.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Holocron.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Holocron.API.dll"]
