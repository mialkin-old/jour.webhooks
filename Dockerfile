FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Jour.Webhooks/Jour.Webhooks.csproj", "src/Jour.Webhooks/"]
RUN dotnet restore "src/Jour.Webhooks/Jour.Webhooks.csproj"
COPY . .
WORKDIR "/src/src/Jour.Webhooks"
RUN dotnet build "Jour.Webhooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jour.Webhooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jour.Webhooks.dll"]