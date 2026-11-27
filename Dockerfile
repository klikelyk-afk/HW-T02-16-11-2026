# ---------- Stage 1: build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Спочатку копіюємо лише .csproj і відновлюємо пакети (цей шар кешується)
COPY PvStudentsWebApi.csproj ./
RUN dotnet restore

# Потім копіюємо решту коду і публікуємо
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---------- Stage 2: runtime ----------
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5001
ENTRYPOINT ["dotnet", "PvStudentsWebApi.dll"]