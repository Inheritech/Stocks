#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Stocks.API/Stocks.API.csproj", "Stocks.API/"]
COPY ["Stocks.Domain/Stocks.Domain.csproj", "Stocks.Domain/"]
COPY ["Stocks.Infrastructure/Stocks.Infrastructure.csproj", "Stocks.Infrastructure/"]
RUN dotnet restore "Stocks.API/Stocks.API.csproj"
COPY . .
WORKDIR "/src/Stocks.API"
RUN dotnet build "Stocks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stocks.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stocks.API.dll"]