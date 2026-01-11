ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:9.0-alpine

FROM ${DOTNET_SDK} AS build
WORKDIR /src
COPY ["FoodChallenge.Order.Api/FoodChallenge.Order.Api.csproj", "FoodChallenge.Order.Api/"]
COPY ["FoodChallenge.Infrastructure.Data.Postgres/FoodChallenge.Infrastructure.Data.Postgres.csproj", "FoodChallenge.Infrastructure.Data.Postgres/"]
COPY ["FoodChallenge.Order.Ioc/FoodChallenge.Order.Ioc.csproj", "FoodChallenge.Order.Ioc/"]
COPY ["FoodChallenge.Order.Application/FoodChallenge.Order.Application.csproj", "FoodChallenge.Order.Application/"]
COPY ["FoodChallenge.Order.Domain/FoodChallenge.Order.Domain.csproj", "FoodChallenge.Order.Domain/"]
COPY ["FoodChallenge.Order.Adapter/FoodChallenge.Order.Adapter.csproj", "FoodChallenge.Order.Adapter/"]
COPY ["FoodChallenge.Common/FoodChallenge.Common.csproj", "FoodChallenge.Common/"]
COPY ["FoodChallenge.Infrastructure.Http/FoodChallenge.Infrastructure.Http.csproj", "FoodChallenge.Infrastructure.Http/"]
RUN dotnet restore "FoodChallenge.Order.Api/FoodChallenge.Order.Api.csproj"
COPY . .

FROM build as migrations
WORKDIR /src
RUN dotnet tool install --version 9.0.5 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project FoodChallenge.Infrastructure.Data.Postgres --startup-project FoodChallenge.Order.Api