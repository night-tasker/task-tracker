FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NightTasker.TaskTracker.Presentation/NightTasker.TaskTracker.Presentation.WebApi/NightTasker.TaskTracker.Presentation.WebApi.csproj", "NightTasker.TaskTracker.Presentation/NightTasker.TaskTracker.Presentation.WebApi/"]
COPY ["NightTasker.TaskTracker.Infrastructure/NightTasker.TaskTracker.Infrastructure.Messaging/NightTasker.TaskTracker.Infrastructure.Messaging.csproj", "NightTasker.TaskTracker.Infrastructure/NightTasker.TaskTracker.Infrastructure.Messaging/"]
COPY ["NightTasker.TaskTracker.Core/NightTasker.TaskTracker.Core.Application/NightTasker.TaskTracker.Core.Application.csproj", "NightTasker.TaskTracker.Core/NightTasker.TaskTracker.Core.Application/"]
COPY ["NightTasker.TaskTracker.Core/NightTasker.TaskTracker.Core.Domain/NightTasker.TaskTracker.Core.Domain.csproj", "NightTasker.TaskTracker.Core/NightTasker.TaskTracker.Core.Domain/"]
COPY ["NightTasker.TaskTracker.Infrastructure/NightTasker.TaskTracker.Infrastructure.Persistence/NightTasker.TaskTracker.Infrastructure.Persistence.csproj", "NightTasker.TaskTracker.Infrastructure/NightTasker.TaskTracker.Infrastructure.Persistence/"]
RUN dotnet restore "NightTasker.TaskTracker.Presentation/NightTasker.TaskTracker.Presentation.WebApi/NightTasker.TaskTracker.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/NightTasker.TaskTracker.Presentation/NightTasker.TaskTracker.Presentation.WebApi"
RUN dotnet build "NightTasker.TaskTracker.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NightTasker.TaskTracker.Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightTasker.TaskTracker.Presentation.WebApi.dll"]
