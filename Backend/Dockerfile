FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY InstrumentPlatform/InstrumentPlatform.csproj InstrumentPlatform/
RUN dotnet restore InstrumentPlatform/InstrumentPlatform.csproj

COPY InstrumentPlatform/ InstrumentPlatform/
RUN dotnet publish InstrumentPlatform/InstrumentPlatform.csproj -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "InstrumentPlatform.dll"]
