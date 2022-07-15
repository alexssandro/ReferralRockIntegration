#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReferralRockIntegration.Web/ReferralRockIntegration.Web.csproj", "ReferralRockIntegration.Web/"]
RUN dotnet restore "ReferralRockIntegration.Web/ReferralRockIntegration.Web.csproj"
COPY . .
WORKDIR "/src/ReferralRockIntegration.Web"
RUN dotnet build "ReferralRockIntegration.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReferralRockIntegration.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReferralRockIntegration.Web.dll"]