FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# get the tailwindcss executable
RUN chmod u+x ./tailwindcss
# Run tailwindcss to generate the main css file (minified)
RUN ./tailwindcss -i ./wwwroot/css/site.css -o ./wwwroot/css/tailwind.css --minify
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "fullstack-portfolio.dll"]
