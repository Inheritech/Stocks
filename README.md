# Stocks.API

This project is a simple stocks API that allows creating an account, and mocks buying and selling any kind of stock.

## How to run

This project can be run in different ways depending on available resources.
On all cases first you need to pull the repository to a local directory.

Since this project requires a database connection it uses MSSQL LocalDB as the default persistence connection, if it's not available for example if running this project on Linux or Docker, the connection string can be overriden with the following environment variable: `ConnectionStrings__StocksDb`


### .NET Core SDK

A .NET Core SDK with at least version 3.1 is required to build and run this project, if you already have .NET SDK installed you can execute:

```bash
dotnet run --project Service/Stocks.API/Stocks.API.csproj
```

To start the application.

### Docker

If you don't want to install any build tools Docker can be used instead although in this case you have to override the database connection string forcefully since the Dockerfile uses a Linux image by default and MSSQL LocalDB is not available on the image.

To build the docker image for the project the following command should be executed from the root of the repository:

```bash
docker build -f Service/Stocks.API/Dockerfile .
```

And then you can run the generated docker image as usual.

### Docker Compose

The easiest way to run the project is using docker-compose, the docker compose project includes SQL Server and the required connection string override.

To run the docker-compose project just execute this at the root of the repository:

```bash
docker-compose up
```

> Note that sometimes the API might show error logs when trying to connect to the database, this is normal, this happens because the SQL Server instance has not fully initialized and is not serving requests yet. The service will keep trying to connect through a retry policy.

## Testing the API

To start using the API you can simply go to the binded application port depending on where you are running the project, in case of Docker Compose the application will try to allocate port 5101.

When opening the application URL you will be redirected to the Swagger UI to easily execute requests directly on the browser.

## Running Unit Tests

The project includes unit tests for all domain entities in the Stocks.Domain.Tests project.
If making any changes you can run the unit tests through the following command:

```bash
dotnet test .\Tests\Stocks.Domain.Tests\Stocks.Domain.Tests.csproj
```