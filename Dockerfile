#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["Sbran.WebApp/Sbran.WebApp.csproj", "Sbran.WebApp/"]
COPY ["Sbran.CQS/Sbran.CQS.csproj", "Sbran.CQS/"]
COPY ["Sbran.Domain/Sbran.Domain.csproj", "Sbran.Domain/"]
COPY ["Sbran.Shared.Contracts/Sbran.Shared.Contracts.csproj", "Sbran.Shared.Contracts/"]
RUN dotnet restore "Sbran.WebApp/Sbran.WebApp.csproj"
COPY . .
WORKDIR "/src/Sbran.WebApp"
RUN dotnet build "Sbran.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sbran.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sbran.WebApp.dll"]