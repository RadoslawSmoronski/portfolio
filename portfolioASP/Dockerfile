FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY portfolioASP/*.csproj portfolioASP/
COPY portfolio.DataAccess/*.csproj portfolio.DataAccess/
COPY portfolio.Models/*.csproj portfolio.Models/
COPY portfolio.Utility/*.csproj portfolio.Utility/

COPY portfolio.sln .

RUN dotnet restore

COPY . .

RUN dotnet publish portfolioASP/portfolioASP.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "portfolioASP.dll"]