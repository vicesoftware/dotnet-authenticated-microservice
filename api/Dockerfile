FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
COPY api.csproj ./
RUN dotnet restore
COPY . .
CMD dotnet run