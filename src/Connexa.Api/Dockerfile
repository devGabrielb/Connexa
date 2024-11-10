FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

USER app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Connexa.Api/Connexa.Api.csproj", "src/Connexa.Api/"]
COPY ["src/Connexa.Application.csproj", "src/Connexa.Application"],
COPY ["src/Connexa.Domain.csproj", "src/Connexa.Domain"],
COPY ["src/Connexa.Infra.csproj", "src/Connexa.Infra]
RUN dotnet restore "src/Connexa.Api/Connexa.Api.csproj"
COPY . .
WORKDIR "/src/src/Connexa.Api"
RUN dotnet build "Connexa.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Connexa.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Connexa.Api.dll"]
