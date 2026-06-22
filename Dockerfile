FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TheReviewer.Frontend/TheReviewer.Frontend.csproj", "TheReviewer.Frontend/"]
COPY ["TheReviewer.Data/TheReviewer.Data.csproj", "TheReviewer.Data/"]
COPY ["TheReviewer.Logic/TheReviewer.Logic.csproj", "TheReviewer.Logic/"]
RUN dotnet restore "TheReviewer.Frontend/TheReviewer.Frontend.csproj"
COPY . .
WORKDIR "/src/TheReviewer.Frontend"
RUN dotnet build "./TheReviewer.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TheReviewer.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheReviewer.Frontend.dll"]
