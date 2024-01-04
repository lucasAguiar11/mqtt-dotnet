FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["mqtt-dotnet-subscribe/mqtt-dotnet-subscribe.csproj", "mqtt-dotnet-subscribe/"]
RUN dotnet restore "mqtt-dotnet-subscribe/mqtt-dotnet-subscribe.csproj"
COPY . .
WORKDIR "/src/mqtt-dotnet-subscribe"
RUN dotnet build "mqtt-dotnet-subscribe.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "mqtt-dotnet-subscribe.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mqtt-dotnet-subscribe.dll"]
