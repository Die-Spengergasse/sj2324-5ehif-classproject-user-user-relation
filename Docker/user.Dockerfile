# Stage 1: Build the app using .NET SDK 7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the project files to the working directory of the Docker image
COPY . .
RUN dotnet publish sj2324-5ehif-cooking-user.Webapi/sj2324-5ehif-cooking-user.Webapi.csproj -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build-env /app/out .

# Set the entry point to the Web API project
ENTRYPOINT ["dotnet", "sj2324-5ehif-cooking-user.Webapi.dll"]