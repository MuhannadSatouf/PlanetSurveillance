# 🌍 Planet Surveillance API

This project is a **Planet Surveillance** system built with ASP.NET Core 8.0, Entity Framework Core, and the Star Wars API (SWAPI). It allows you to track visits of people to different planets in the Star Wars universe, retrieve information from SWAPI, and store the data in a SQL Server database.

## 📋 Table of Contents

- [Getting Started](#getting-started)
- [Technologies Used](#technologies-used)
- [Database](#database)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Running Tests](#running-tests)
- [API Endpoints](#api-endpoints)
- [Logging](#logging)

## 🚀 Getting Started

### Prerequisites

To run this project, you will need the following:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or a local version like SQL Server Express)
- Any code editor, such as [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Installation

1. **Clone the repository**:

    ```bash
    git clone https://github.com/MuhannadSatouf/PlanetSurveillance.git
    cd PlanetSurveillance
    ```

2. **Install the dependencies**:

    In the main project folder, run the following command to restore all NuGet packages:

    ```bash
    dotnet restore
    ```

3. **Database Migration**:

    Apply the migrations to set up the database:

    ```bash
    dotnet ef database update
    ```

    Make sure your connection string is properly configured in the `appsettings.json` or `appsettings.Development.json` file.

---

## 🛠️ Technologies Used

This project is built using the following tools and technologies:

- **.NET Core 8.0**
- **ASP.NET Core** (Web API)
- **Entity Framework Core** (EF Core) with SQL Server
- **xUnit** (for Unit Testing)
- **MoQ** (for mocking in tests)
- **HttpClient** (for SWAPI integration)
- **Swagger** (API Documentation)
- **Newtonsoft.Json** (for JSON serialization)

---

## 🗄️ Database

I use **SQL Server** for this project. Entity Framework Core is used for database management and migrations. The database stores information about **Persons**, **Planets**, and **Visits**.

- **Person Table**: Stores information about the people visiting planets.
- **Planet Table**: Stores data about different planets.
- **Visit Table**: Tracks when and where a person has visited a planet.

---

## 🔧 Configuration

The project uses two configuration files:

- **`appsettings.json`**: Default configuration, mainly used for production.
- **`appsettings.Development.json`**: Used for development environment.

### Example of `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=PlanetSurveillanceDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Information",
      "PlanetSurveillance.Services.SwapiService": "Debug"
    }
  },
  "AllowedHosts": "*"
}
