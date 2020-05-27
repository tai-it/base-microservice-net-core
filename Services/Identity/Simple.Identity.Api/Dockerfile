#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Identity/Simple.Identity.Api/Simple.Identity.Api.csproj", "Services/Identity/Simple.Identity.Api/"]
RUN dotnet restore "Services/Identity/Simple.Identity.Api/Simple.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Simple.Identity.Api"
RUN dotnet build "Simple.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple.Identity.Api.dll"]