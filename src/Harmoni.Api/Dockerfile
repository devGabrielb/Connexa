FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

USER app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Harmoni.Api/Harmoni.Api.csproj", "src/Harmoni.Api/"]
COPY ["src/Harmoni.Application/Harmoni.Application.csproj", "src/Harmoni.Application/"]
COPY ["src/Harmoni.Domain/Harmoni.Domain.csproj", "src/Harmoni.Domain/"]
COPY ["src/Harmoni.Infra/Harmoni.Infra.csproj", "src/Harmoni.Infra/"]
RUN dotnet restore "./src/Harmoni.Api/Harmoni.Api.csproj"
COPY . .
WORKDIR "/src/src/Harmoni.Api"
RUN dotnet build "./Harmoni.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./Harmoni.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harmoni.Api.dll"]
