# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj and restore as distinct layers
COPY ["Catalog.API/Catalog.API.csproj", "./"]
RUN dotnet restore "Catalog.API.csproj"

# Copy the remaining source code and build the project
COPY . .
WORKDIR "/src"
RUN dotnet build "Catalog.API/Catalog.API.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Catalog.API/Catalog.API.csproj" -c Release -o /app/publish

# Final image for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
