#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Bookings/Booking.API/Booking.API.csproj", "src/Services/Bookings/Booking.API/"]
RUN dotnet restore "src/Services/Bookings/Booking.API/Booking.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Bookings/Booking.API"
RUN dotnet build "Booking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Booking.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Booking.API.dll"]