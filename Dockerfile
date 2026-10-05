# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
SHELL ["powershell", "-NoProfile", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\src
COPY NONBAOHIEMVIETTIN\ .\

RUN nuget restore .\NONBAOHIEMVIETTIN.sln -NonInteractive
RUN msbuild .\NONBAOHIEMVIETTIN\NONBAOHIEMVIETTIN.csproj /p:Configuration=Release /p:DeployOnBuild=true /p:WebPublishMethod=FileSystem /p:DeleteExistingFiles=true /p:publishUrl=C:\out

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8
WORKDIR C:\inetpub\wwwroot
RUN powershell -NoProfile -Command "Remove-Item -Recurse -Force C:\inetpub\wwwroot\*"
COPY --from=build C:\out\ .
EXPOSE 80
