
# Base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the project
COPY . ./
RUN dotnet publish -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables if needed
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the necessary ports
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT ["dotnet", "WireGuardManager.Api.dll"]
