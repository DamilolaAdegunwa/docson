FROM microsoft/dotnet:2.2.2-aspnetcore-runtime-alpine3.8
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source} .
VOLUME /app/wwwroot/data
ENTRYPOINT ["dotnet", "tomware.Docson.dll"]