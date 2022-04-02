# Buildersoft Andy X Portal Docker File

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Andy.X.Portal/Andy.X.Portal.csproj", "src/Andy.X.Portal/"]
RUN dotnet restore "src/Andy.X.Portal/Andy.X.Portal.csproj"
COPY . .
WORKDIR "/src/src/Andy.X.Portal"
RUN dotnet build "Andy.X.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Andy.X.Portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Andy.X.Portal.dll"]