FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["jcBENCH.MVC/jcBENCH.MVC.csproj", "jcBENCH.MVC/"]
RUN dotnet restore "./jcBENCH.MVC/jcBENCH.MVC.csproj"
COPY . .
WORKDIR "/src/jcBENCH.MVC"
RUN dotnet build "./jcBENCH.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./jcBENCH.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jcBENCH.MVC.dll"]