# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["samples/3.EndPoints/Base.Samples.EndPoints.WebApi/Base.Samples.EndPoints.WebApi.csproj", "samples/3.EndPoints/Base.Samples.EndPoints.WebApi/"]
COPY ["src/2.Core/Base.Core.ApplicationServices/Base.Core.ApplicationServices.csproj", "src/2.Core/Base.Core.ApplicationServices/"]
COPY ["src/1.Utilities/Base.Utility/Base.Utility.csproj", "src/1.Utilities/Base.Utility/"]
COPY ["src/2.Core/Base.Core.Contracts/Base.Core.Contracts.csproj", "src/2.Core/Base.Core.Contracts/"]
COPY ["src/2.Core/Base.Core.Domains/Base.Core.Domains.csproj", "src/2.Core/Base.Core.Domains/"]
COPY ["src/2.Core/Base.Core.RequestResponse/Base.Core.RequestResponse.csproj", "src/2.Core/Base.Core.RequestResponse/"]
COPY ["src/4.EndPoints/Base.EndPoints.Web/Base.EndPoints.Web.csproj", "src/4.EndPoints/Base.EndPoints.Web/"]
COPY ["src/3.Infra/Data/Base.Infrastructure/Base.Infrastructure.csproj", "src/3.Infra/Data/Base.Infrastructure/"]
COPY ["samples/1.Core/Base.Samples.Core.ApplicationServices/Base.Samples.Core.ApplicationServices.csproj", "samples/1.Core/Base.Samples.Core.ApplicationServices/"]
COPY ["samples/1.Core/Base.Samples.Core.Contracts/Base.Samples.Core.Contracts.csproj", "samples/1.Core/Base.Samples.Core.Contracts/"]
COPY ["samples/1.Core/Base.Samples.Core.Domain/Base.Samples.Core.Domain.csproj", "samples/1.Core/Base.Samples.Core.Domain/"]
COPY ["samples/2.Infra/Data/Base.Samples.Infrastructure/Base.Samples.Infrastructure.csproj", "samples/2.Infra/Data/Base.Samples.Infrastructure/"]
RUN dotnet restore "./samples/3.EndPoints/Base.Samples.EndPoints.WebApi/Base.Samples.EndPoints.WebApi.csproj"
COPY . .
WORKDIR "/src/samples/3.EndPoints/Base.Samples.EndPoints.WebApi"
RUN dotnet build "./Base.Samples.EndPoints.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Base.Samples.EndPoints.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Base.Samples.EndPoints.WebApi.dll"]