FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 44300

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/CompanyName.ProjectName.Api/CompanyName.ProjectName.Api.csproj src/CompanyName.ProjectName.Api/
RUN dotnet restore src/CompanyName.ProjectName.Api/CompanyName.ProjectName.Api.csproj
COPY . .
WORKDIR /src/src/CompanyName.ProjectName.Api
RUN dotnet build CompanyName.ProjectName.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CompanyName.ProjectName.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CompanyName.ProjectName.Api.dll"]
