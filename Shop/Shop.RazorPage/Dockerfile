#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop/Shop.RazorPage/Shop.RazorPage.csproj", "Shop/Shop.RazorPage/"]
RUN dotnet restore "Shop/Shop.RazorPage/Shop.RazorPage.csproj"
COPY . .
WORKDIR "/src/Shop/Shop.RazorPage"
RUN dotnet build "Shop.RazorPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.RazorPage.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.RazorPage.dll"]