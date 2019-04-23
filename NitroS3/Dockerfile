FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY NitroS3/NitroS3.csproj NitroS3/
RUN dotnet restore NitroS3/NitroS3.csproj
COPY . .
WORKDIR /src/NitroS3
RUN dotnet build NitroS3.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NitroS3.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NitroS3.dll"]


#docker run -it -e MINIO_ACCESS_KEY=AKIAIOSFODNN7EXAMPLE -e MINIO_SECRET_KEY=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY -p 9000:9000 minio/minio server /data