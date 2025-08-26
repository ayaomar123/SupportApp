# ----- Build Stage -----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /build


# Copy project files for restore
COPY ["src/SupportApp.Api/SupportApp.Api.csproj", "src/SupportApp.Api/"]
COPY ["src/SupportApp.Application/SupportApp.Application.csproj", "src/SupportApp.Application/"]
COPY ["src/SupportApp.Domain/SupportApp.Domain.csproj", "src/SupportApp.Domain/"]
COPY ["src/SupportApp.Infrastructure/SupportApp.Infrastructure.csproj", "src/SupportApp.Infrastructure/"]
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]

# Restore dependencies (only once)
RUN dotnet restore "src/SupportApp.Api/SupportApp.Api.csproj"

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish "src/SupportApp.Api/SupportApp.Api.csproj" -c Release -o /app

# ----- Final Stage -----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Install timezone data for TimeZoneInfo support
RUN apt-get update && apt-get install -y tzdata && \
    ln -fs /usr/share/zoneinfo/America/Montreal /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    rm -rf /var/lib/apt/lists/*

ENV TZ=America/Montreal

WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "SupportApp.Api.dll"]