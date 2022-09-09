#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM nginx:alpine
WORKDIR /etc/nginx
COPY ./nginx.conf ./conf.d/default.conf
ENTRYPOINT [ "nginx" ]
CMD [ "-g", "daemon off;" ]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Gateway/Gateway.API/Gateway.API.csproj", "src/Services/Gateway/Gateway.API/"]
RUN dotnet restore "src/Services/Gateway/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Gateway/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]