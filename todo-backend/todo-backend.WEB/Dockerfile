
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["todo-backend.WEB/todo-backend.WEB.csproj", "todo-backend.WEB/"]
COPY ["todo-backend.Infrastructure/todo-backend.Infrastructure.csproj", "todo-backend.Infrastructure/"]
COPY ["todo-backend.Domain/todo-backend.Domain.csproj", "todo-backend.Domain/"]
RUN dotnet restore "./todo-backend.WEB/todo-backend.WEB.csproj"
COPY . .
WORKDIR "/src/todo-backend.WEB"
RUN dotnet build "./todo-backend.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./todo-backend.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo-backend.WEB.dll"]