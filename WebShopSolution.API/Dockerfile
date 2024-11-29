# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and .csproj files first to restore dependencies
COPY ["WebShopSolution.API/WebShopSolution.API.csproj", "./WebShopSolution.API/"]
COPY ["WebShopSolution.Application/WebShopSolution.Application.csproj", "./WebShopSolution.Application/"]
COPY ["WebShopSolution.Infrastructure/WebShopSolution.Infrastructure.csproj", "./WebShopSolution.Infrastructure/"]
COPY ["WebShopSolution.Services/WebShopSolution.Services.csproj", "./WebShopSolution.Services/"]
COPY ["WebShopSolution.Domain/WebShopSolution.Domain.csproj", "./WebShopSolution.Domain/"]
COPY ["WebShopSolution.Tests/WebShopSolution.Tests.csproj", "./WebShopSolution.Tests/"]

# Restore 
RUN dotnet restore "WebShopSolution.API/WebShopSolution.API.csproj"

# Copy the entire solution to the container
COPY . .

# Publish the API project (this is the entry point for your Docker image)
RUN dotnet build "WebShopSolution.API/WebShopSolution.API.csproj" -c Release 

# Build the runtime image
FROM build AS test
WORKDIR /src/WebShopSolution.Tests

RUN dotnet test "WebShopSolution.Tests.csproj" --no-build --logger:trx --results-directory /src/TestResults

FROM build AS publish
WORKDIR /src/WebShopSolution.API
RUN dotnet publish "WebShopSolution.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Set the entry point to the API project DLL
ENTRYPOINT ["dotnet", "WebShopSolution.API.dll"]
