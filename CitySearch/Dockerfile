FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY /CitySearch /src
WORKDIR /src
RUN dotnet build "CitySearch.csproj" -c Release

FROM build AS publish
RUN dotnet publish "CitySearch.csproj" -c Release

FROM base AS final
COPY --from=publish /src/bin/Release/net7.0/publish .
ENTRYPOINT ["dotnet", "CitySearch.dll"]