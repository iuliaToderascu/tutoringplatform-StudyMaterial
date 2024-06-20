# Use the .NET SDK base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory within the container
WORKDIR /app

# Copy the entire project directory from the host machine
COPY . .

# Restore dependencies for your project
RUN dotnet restore

# Build the project in Release configuration and output to /app/build
RUN dotnet build -c Release -o /app/build

# (Optional) Create a publish stage for deployment (if needed)
# ... (add publish stage instructions here if necessary)

# Use the aspnet base image for runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Copy the published files from the build stage (if using publish stage)
# COPY --from=publish /app/publish .  # Assuming you have a publish stage

# Set the working directory within the final image
WORKDIR /app

# (Optional) If using publish stage, comment out the following line
# COPY . .  # Only needed if not using a publish stage

# Set the entrypoint to run your application
ENTRYPOINT ["dotnet", "TutoringPlatformBackEnd.StudyMaterial.dll"]
