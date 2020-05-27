#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Catalog/Simple.Catalog.Api/Simple.Catalog.Api.csproj", "Services/Catalog/Simple.Catalog.Api/"]
COPY ["Sharing/Simple.EventBus/Simple.EventBus.csproj", "Sharing/Simple.EventBus/"]
COPY ["Sharing/Simple.Core/Simple.Core.csproj", "Sharing/Simple.Core/"]
RUN dotnet restore "Services/Catalog/Simple.Catalog.Api/Simple.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Simple.Catalog.Api"
RUN dotnet build "Simple.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple.Catalog.Api.dll"]