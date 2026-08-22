FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "WalletApi/WalletApi.csproj"
RUN dotnet publish "WalletApi/WalletApi.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=true

EXPOSE 8080

ENTRYPOINT ["dotnet", "WalletApi.dll"]
