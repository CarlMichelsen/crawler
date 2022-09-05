FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["crawler.sln", "./"]

COPY . .

RUN dotnet restore

WORKDIR "/src/."

# this is not in use
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet restore "./Api/Api.csproj"

RUN dotnet build "./Api/Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "./Api.dll"]