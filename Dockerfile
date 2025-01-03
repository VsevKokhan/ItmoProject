
# Базовый образ для выполнения приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решений и проектов
COPY BackendServer.sln ./
COPY Backend/Backend.csproj Backend/
COPY Data/Data.csproj Data/
COPY Interfaces/Interfaces.csproj Interfaces/
COPY Services/Services.csproj Services/
COPY Models/Models.csproj Models/
# Восстанавливаем зависимости
RUN dotnet restore BackendServer.sln

# Копируем весь исходный код
COPY . .

# Сборка основного проекта
WORKDIR /src/Backend
RUN dotnet build Backend.csproj -c Release -o /app

# Публикация основного проекта
FROM build AS publish
RUN dotnet publish Backend.csproj -c Release -o /app

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# Точка входа
ENTRYPOINT ["dotnet", "Backend.dll"]
