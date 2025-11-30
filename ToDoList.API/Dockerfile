FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ToDoList.API/TodoList.Api/*.csproj ./ToDoList.Api/
RUN dotnet restore ./ToDoList.Api/TodoList.Api.csproj

# Copy everything and publish
COPY . .
RUN dotnet publish ToDoList.API/TodoList.Api/TodoList.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "TodoList.Api.dll"]
