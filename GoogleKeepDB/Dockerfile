FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55495
EXPOSE 44379

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY GoogleKeepDB/GoogleKeepDB.csproj GoogleKeepDB/
RUN dotnet restore GoogleKeepDB/GoogleKeepDB.csproj
COPY . .
WORKDIR /src/GoogleKeepDB
RUN dotnet build GoogleKeepDB.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GoogleKeepDB.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GoogleKeepDB.dll"]
