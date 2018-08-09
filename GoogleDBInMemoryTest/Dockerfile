FROM microsoft/dotnet:2.1-runtime-nanoserver-1709 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY GoogleDBInMemoryTest/GoogleDBInMemoryTest.csproj GoogleDBInMemoryTest/
COPY GoogleKeepDB/GoogleKeepDB.csproj GoogleKeepDB/
RUN dotnet restore GoogleDBInMemoryTest/GoogleDBInMemoryTest.csproj
COPY . .
WORKDIR /src/GoogleDBInMemoryTest
RUN dotnet build GoogleDBInMemoryTest.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GoogleDBInMemoryTest.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GoogleDBInMemoryTest.dll"]
