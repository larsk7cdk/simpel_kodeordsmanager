FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/simpel_kodeordsmanager.api/simpel_kodeordsmanager.api.csproj", "src/simpel_kodeordsmanager.api/"]
RUN dotnet restore "src/simpel_kodeordsmanager.api/simpel_kodeordsmanager.api.csproj"
COPY . .
WORKDIR "/src/src/simpel_kodeordsmanager.api"
RUN dotnet build "./simpel_kodeordsmanager.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./simpel_kodeordsmanager.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "simpel_kodeordsmanager.api.dll"]
