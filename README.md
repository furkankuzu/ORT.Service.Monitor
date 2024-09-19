# ORT.Service.Monitor

ORT.Service.Monitor is a web application that monitors websites and notifies users via Quartz jobs. This repository includes everything needed to set up and run the service locally for development and debugging.

## Features

- Add websites to monitor via an API
- Periodic monitoring of websites using Quartz jobs
- Notifications when websites are down
- Built with ASP.NET Core and Entity Framework Core
- SQL Server Docker container setup for the database

## Prerequisites

Before you can clone and run the project, ensure you have the following installed on your machine:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/products/docker-desktop) (for running the SQL Server container)
- [Git](https://git-scm.com/downloads)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

Follow these steps to get the project running locally for development and debugging.

### 1. Clone the Repository

```bash
git clone https://github.com/furkankuzu/ORT.Service.Monitor.git
cd ORT.Service.Monitor
```

### 2. Set Up SQL Server with Docker
The project uses SQL Server, which can be run in a Docker container. If you have Docker installed, use the provided `docker-compose.yml` to start the database.

Run the following command in the **docker folder**:

```bash
docker-compose up -d
```
This will start a SQL Server container.

### 3. Configure Connection Strings
Ensure that the connection string in `appsettings.json` is set correctly for your Docker SQL Server instance. If using Docker, the connection string should be:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=WebsiteMonitor;User Id=sa;Password=Friday01!;TrustServerCertificate=True;"
  }
}

```

Make sure the password matches the one in your `docker-compose.yml`.

### 4. Run the Application
You can run the application using the .NET CLI or Visual Studio.
```dotnet
dotnet run
```
