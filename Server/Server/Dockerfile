FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
EXPOSE 80/tcp
ENV ASPNETCORE_URLS https://*:80

WORKDIR /code/app

COPY Server.csproj .
COPY Directory.Build.props .
RUN dotnet restore


ENTRYPOINT dotnet watch run --no-restore