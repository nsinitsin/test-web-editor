FROM microsoft/dotnet:2.2.1-aspnetcore-runtime
WORKDIR /app
COPY ./publish .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Archangel.Tests.WebEditor.dll"]