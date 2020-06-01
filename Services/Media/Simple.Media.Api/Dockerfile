#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Media/Simple.Media.Api/Simple.Media.Api.csproj", "Services/Media/Simple.Media.Api/"]
COPY ["Sharing/Simple.Core/Simple.Core.csproj", "Sharing/Simple.Core/"]
RUN dotnet restore "Services/Media/Simple.Media.Api/Simple.Media.Api.csproj"
COPY . .
WORKDIR "/src/Services/Media/Simple.Media.Api"
RUN dotnet build "Simple.Media.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple.Media.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple.Media.Api.dll"]