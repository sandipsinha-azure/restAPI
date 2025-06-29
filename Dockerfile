
# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyAwsApi.csproj", "./"]
RUN dotnet restore "MyAwsApi.csproj"
COPY . .
RUN dotnet build "MyAwsApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MyAwsApi.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAwsApi.dll"]
