FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/NonBaoHiemVietTin/NonBaoHiemVietTin.csproj src/NonBaoHiemVietTin/
RUN dotnet restore src/NonBaoHiemVietTin/NonBaoHiemVietTin.csproj

COPY src/NonBaoHiemVietTin/ src/NonBaoHiemVietTin/
RUN dotnet publish src/NonBaoHiemVietTin/NonBaoHiemVietTin.csproj -c Release -o /app/publish --no-restore
COPY NONBAOHIEMVIETTIN/NONBAOHIEMVIETTIN/assets/ /app/publish/wwwroot/assets/
COPY NONBAOHIEMVIETTIN/NONBAOHIEMVIETTIN/Content/ /app/publish/wwwroot/Content/
COPY NONBAOHIEMVIETTIN/NONBAOHIEMVIETTIN/Scripts/ /app/publish/wwwroot/Scripts/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "NonBaoHiemVietTin.dll"]
