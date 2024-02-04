FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LMS.WebAPI/LMS.WebAPI.csproj", "LMS.WebAPI/"]
RUN dotnet restore "LMS.WebAPI/LMS.WebAPI.csproj"
COPY . .
WORKDIR "/src/LMS.WebAPI"
RUN dotnet build "LMS.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LMS.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LMS.WebAPI.dll"]
