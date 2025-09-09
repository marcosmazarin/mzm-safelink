# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /source

# Copia apenas a solution e os csproj primeiro para otimizar cache
COPY *.sln ./
COPY mzm-safelink.api/*.csproj mzm-safelink.api/
COPY mzm-safelink.application/*.csproj mzm-safelink.application/
COPY mzm-safelink.domain/*.csproj mzm-safelink.domain/
COPY mzm-safelink.infra/*.csproj mzm-safelink.infra/
COPY mzm-safelink.ioc/*.csproj mzm-safelink.ioc/

# Restaura pacotes
RUN dotnet restore

# Agora copia todo o restante do código
COPY . .

# Build e publish
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish mzm-safelink.api/mzm-safelink.api.csproj -c Release -o /app

################################################################################
# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "mzm-safelink.api.dll"]
