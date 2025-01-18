FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ARG REST_PORT
ENV REST_PORT $REST_PORT

ARG databaseConnectionString
ENV ConnectionString $databaseConnectionString

ARG gRPC_PORT
ENV gRPC_PORT $GRPC_PORT

USER $APP_UID
WORKDIR /app
EXPOSE $REST_PORT
EXPOSE $GRPC_PORT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.JWT/Infrastructure.Tokens.JWT.csproj", "Infrastructure.JWT/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Persistense/Persistense.csproj", "Persistense/"]
COPY ["Infrastructure.Tokens.AspNet/Infrastructure.Tokens.AspNetCore.csproj", "Infrastructure.Tokens.AspNet/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
