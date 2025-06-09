# CountryGwpApi

## Overview
CountryGwpApi is a self-hosted .NET 9 Web API for calculating the average Gross Written Premium (GWP) by country and line of business (LOB) over a specified period. 
The API loads data from a CSV file into memory on startup and exposes endpoints for querying average GWP values.

## Features
- Clean Architecture (Domain, Application, Infrastructure, API layers)
- SOLID principles and dependency injection
- Asynchronous API and repository
- OpenAPI documentation with Scalar UI
- Docker support
- Easily configurable year range for calculations
- In-memory caching for average GWP results to improve performance
- End-to-end (E2E) tests for full API scenario validation

## How to Run

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/) (for containerized run)

### 1. Clone the repositorygit clone <your-repo-url>
cd CountryGwpApi
### 2. Run with .NET CLIdotnet build CountryGwpApi/CountryGwpApi.csproj
cd CountryGwpApi
DOTNET_ENVIRONMENT=Development dotnet runThe API will be available at [http://localhost:9091/openapi/ui](http://localhost:9091/openapi/ui)

### 3. Prepare the data
Ensure the file `gwpByCountry.csv` is located in `CountryGwpApi/Data/`.

### 4. Run with Docker
Build and run the container:docker build -t countrygwpapi:latest -f CountryGwpApi/Dockerfile .
`docker run -p 9091:9091 countrygwpapi:latest`
The API will be available at [http://localhost:9091/scalar/v1](http://localhost:9091/scalar/v1)

### 5. Run from Visual Studio
- Open the solution in Visual Studio 2022+
- Select the `Docker Compose` profile and run (F5)

## API Usage

### EndpointPOST /server/api/gwp/avg
Content-Type: application/json
#### Request Body Example{
  "country": "ae",
  "lob": ["property", "transport"],
  "fromYear": 2008,
  "toYear": 2015
}
#### Response Example{
  "property": 446001906.1,
  "transport": 634545022.9
}
- If `fromYear` or `toYear` are omitted or set to 0, defaults are 2008 and 2015.
- OpenAPI (AKA Swagger) is available at `/scalar/v1` for interactive testing.

## Testing
- **Unit tests** are located in the `CountryGwp.Tests` project.
- **End-to-end (E2E) tests** are located in `CountryGwp.Tests/e2e/CountryGwpApiE2ETests.cs` and validate the full API pipeline using an in-memory test server.
- Run all tests with: `dotnet test CountryGwp.Tests/CountryGwp.Tests.csproj`
## Caching
- The application implements in-memory caching for average GWP calculation results.
- Caching is used automatically for repeated requests with the same parameters (country, lob, year range) to improve performance.
