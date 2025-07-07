# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

#  Aqui está o ponto crítico: ajuste o caminho para onde está o .csproj
COPY ["Api-IngaTasks/Api-IngaTasks.csproj", "Api-IngaTasks/"]

RUN dotnet restore "Api-IngaTasks/Api-IngaTasks.csproj"
COPY . .
WORKDIR "/src/Api-IngaTasks"
RUN dotnet build "Api-IngaTasks.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api-IngaTasks.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api-IngaTasks.dll"]
