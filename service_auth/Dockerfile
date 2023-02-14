# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . ./ 
RUN dotnet restore --use-current-runtime  

# copy everything else and build app 
RUN dotnet publish -c Release -o /app --use-current-runtime --self-contained false --no-restore  

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
 
EXPOSE 80
ENTRYPOINT ["dotnet", "service_auth.dll"  ]