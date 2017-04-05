FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "AuthServer.Host.dll"]
