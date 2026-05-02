FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

COPY priorizzeProject/priorizzeProject.csproj priorizzeProject/
RUN dotnet restore priorizzeProject/priorizzeProject.csproj

COPY . .
RUN dotnet publish priorizzeProject/priorizzeProject.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5120
EXPOSE 5120

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "priorizzeProject.dll"]
