FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /src

#Restore NuGet packages so they are cached when we start the container
COPY ./Alloy.1.csproj .
COPY ./Directory.Build.props .
COPY ./nuget.config .

RUN dotnet restore

EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

ENTRYPOINT dotnet run --no-launch-profile
