FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ActivService/ActivService.csproj", "ActivService/"]
RUN dotnet restore "ActivService/ActivService.csproj"
COPY . .
WORKDIR "/src/ActivService"
RUN dotnet build "ActivService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ActivService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivService.dll"]
