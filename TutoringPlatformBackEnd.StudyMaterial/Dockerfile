#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TutoringPlatformBackEnd.StudyMaterial/TutoringPlatformBackEnd.StudyMaterial.csproj", "TutoringPlatformBackEnd.StudyMaterial/"]
RUN dotnet restore "TutoringPlatformBackEnd.StudyMaterial/TutoringPlatformBackEnd.StudyMaterial.csproj"
COPY . .
WORKDIR "/src/TutoringPlatformBackEnd.StudyMaterial"
RUN dotnet build "TutoringPlatformBackEnd.StudyMaterial.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TutoringPlatformBackEnd.StudyMaterial.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TutoringPlatformBackEnd.StudyMaterial.dll"]