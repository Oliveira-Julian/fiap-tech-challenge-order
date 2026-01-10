ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:9.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:9.0

FROM ${DOTNET_RUNTIME} AS base
USER $APP_UID
WORKDIR /app

FROM ${DOTNET_SDK} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "FoodChallenge.Order.Api/FoodChallenge.Order.Api.csproj"
WORKDIR "/src/FoodChallenge.Order.Api"
RUN dotnet build "FoodChallenge.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as migrations
WORKDIR /
RUN dotnet tool install --version 9.0.5 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project src/FoodChallenge.Infrastructure.Data.Postgres --startup-project src/FoodChallenge.Order.Api