FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AltelService/AltelService.csproj", "AltelService/"]
RUN dotnet restore "AltelService/AltelService.csproj"
COPY . .
WORKDIR "/src/AltelService"
RUN dotnet build "AltelService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AltelService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AltelService.dll"]
