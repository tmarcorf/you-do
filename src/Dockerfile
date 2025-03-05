# Use a imagem oficial do .NET SDK 8 para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie todos os arquivos do projeto
COPY . .

# Restaure os pacotes NuGet
RUN dotnet restore "YouDo.API/YouDo.API.csproj"

# Publique a aplicação
RUN dotnet publish "YouDo.API/YouDo.API.csproj" -c Release -o /app

# Use a imagem oficial do .NET runtime 8 para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "YouDo.API.dll"]