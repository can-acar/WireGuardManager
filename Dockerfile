﻿
#FROM build AS publish
#RUN dotnet publish "WireGuardManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .

#

#WORKDIR /src
#COPY ["WireGuardManager.Api/WireGuardManager.Api.csproj", "WireGuardManager.Api/"]
#COPY ["WireGuardManager.Application/WireGuardManager.Application.csproj", "WireGuardManager.Application/"]
#COPY ["WireGuardManager.Domain/WireGuardManager.Domain.csproj", "WireGuardManager.Domain/"]
#COPY ["WireGuardManager.Infrastructure/WireGuardManager.Infrastructure.csproj", "WireGuardManager.Infrastructure/"]
#RUN dotnet restore "WireGuardManager.Api/WireGuardManager.Api.csproj"
#COPY . .
#WORKDIR "/src/WireGuardManager.Api"
#RUN dotnet build "WireGuardManager.Api.csproj" -c Release -o /app/build
#


FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build-env
WORKDIR /app

EXPOSE 80
EXPOSE 443
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore


COPY . .
RUN dotnet publish -c Debug -o out


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables if needed
ENV ASPNETCORE_ENVIRONMENT=Development



ENTRYPOINT ["dotnet", "WireGuardManager.Api.dll"]