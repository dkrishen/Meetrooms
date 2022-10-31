#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./SignalR.API.csproj", "."]
RUN dotnet restore "SignalR.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "SignalR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalR.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalR.API.dll"]