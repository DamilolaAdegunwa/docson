FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/docson.csproj", "src/"]
RUN dotnet restore "src/docson.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "docson.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "docson.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
VOLUME /app/wwwroot/data
ENTRYPOINT ["dotnet", "tomware.Docson.dll"]