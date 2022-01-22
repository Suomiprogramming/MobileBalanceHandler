FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tele2Service/Tele2Service.csproj", "Tele2Service/"]
RUN dotnet restore "Tele2Service/Tele2Service.csproj"
COPY . .
WORKDIR "/src/Tele2Service"
RUN dotnet build "Tele2Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tele2Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tele2Service.dll"]
