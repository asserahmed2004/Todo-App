from mcr.microsoft.com/dotnet/aspnet:9.0 AS base
workdir /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
arg build_configuration=Release
workdir /src
copy ["Todo App/Todo App.csproj", "Todo App/"]
run dotnet restore "Todo App/Todo App.csproj"
copy . .
workdir "/src/Todo App"
run dotnet build "Todo App.csproj" -c $build_configuration -o /app/build

FROM build AS publish
arg build_configuration=Release
run dotnet publish "Todo App.csproj" -c $build_configuration -o /app/publish /p:UseAppHost=false


from base AS final
workdir /app
copy --from=publish /app/publish .
entrypoint ["dotnet", "Todo App.dll"]