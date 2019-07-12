# Base image is Alpine with latest stale Node installed. 
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9
# set working directory for subsequent commands
WORKDIR /app
# leverage build cache by copying npm package files first
COPY ./Finances.WebApp/publish/* ./
ENTRYPOINT ["dotnet", "Finances.WebApp.dll"]
