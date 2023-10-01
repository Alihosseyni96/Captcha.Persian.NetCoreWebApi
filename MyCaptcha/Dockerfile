#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyCaptcha/MyCaptcha.csproj", "MyCaptcha/"]
COPY ["CaptchaConfigurations/Captcha.csproj", "CaptchaConfigurations/"]
RUN dotnet restore "MyCaptcha/MyCaptcha.csproj"
COPY . .
WORKDIR "/src/MyCaptcha"
RUN dotnet build "MyCaptcha.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCaptcha.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY Fonts /usr/local/share/fonts
RUN apt-get update && apt-get install -y fontconfig
RUN fc-cache -f -v


ENTRYPOINT ["dotnet", "MyCaptcha.dll"]