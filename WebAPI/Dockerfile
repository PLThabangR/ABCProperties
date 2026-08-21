# ==========================================
# BASE
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080
EXPOSE 8081


# ==========================================
# BUILD
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src


# ------------------------------------------
# Copy project files first
# ------------------------------------------

COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]


# ------------------------------------------
# Restore dependencies
# ------------------------------------------

RUN dotnet restore "WebAPI/WebAPI.csproj"


# ------------------------------------------
# Copy the rest of the source code
# ------------------------------------------

COPY . .


# ------------------------------------------
# Move into WebAPI project
# ------------------------------------------

WORKDIR "/src/WebAPI"


# ------------------------------------------
# Build
# ------------------------------------------

RUN dotnet build "WebAPI.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build


# ==========================================
# PUBLISH
# ==========================================
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "WebAPI.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


# ==========================================
# FINAL
# ==========================================
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebAPI.dll"]