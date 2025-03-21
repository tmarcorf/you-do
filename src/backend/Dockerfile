# Use a imagem oficial do .NET SDK 8 para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/backend

# Copie apenas os projetos necessários
COPY ["YouDo.API/YouDo.API.csproj", "YouDo.API/"]
COPY ["YouDo.Application/YouDo.Application.csproj", "YouDo.Application/"]
COPY ["YouDo.Core/YouDo.Core.csproj", "YouDo.Core/"]
COPY ["YouDo.Infraestructure/YouDo.Infraestructure.csproj", "YouDo.Infraestructure/"]

# Restaure os pacotes NuGet
RUN dotnet restore "YouDo.API/YouDo.API.csproj"

# Copie o código-fonte dos projetos
COPY ["YouDo.API/", "YouDo.API/"]
COPY ["YouDo.Application/", "YouDo.Application/"]
COPY ["YouDo.Core/", "YouDo.Core/"]
COPY ["YouDo.Infraestructure/", "YouDo.Infraestructure/"]

# Publique a aplicação
RUN dotnet publish "YouDo.API/YouDo.API.csproj" -c Release -o /app

# Use a imagem oficial do .NET runtime 8 para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "YouDo.API.dll"]
