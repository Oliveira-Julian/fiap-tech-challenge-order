ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:9.0-alpine
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:9.0-alpine

FROM ${DOTNET_RUNTIME} AS base
USER $APP_UID
WORKDIR /app

FROM ${DOTNET_SDK} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "FoodChallenge.Order.Api/FoodChallenge.Order.Api.csproj"
WORKDIR "/src/FoodChallenge.Order.Api"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FoodChallenge.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodChallenge.Order.Api.dll"]
