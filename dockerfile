FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build  
  #usa image docker ufficiale 8.0
WORKDIR /src
  #lavora dentro cartella src/
COPY . .  #copia tutto dentro il suo container src/

RUN dotnet restore src/MyPrj.API/MyPrj.API.csproj  #scarica i nuget packages
RUN dotnet publish src/MyPrj.API/MyPrj.API.csproj -c Release -o /app/publish
  #dotnet publish fa compila il prj->crea i files eseguibili -> copia le dipendenze.
#.API è l'entrypoitn, non menzioni gli altri assemblies e.g. .Infrastrucure, perche intanto sono libraries c# (non hanno app.Run() !), cmnq anche gli altri assemblies(e.g. .Infrastructure) vengono compilati.

FROM mcr.microsoft.com/dotnet/aspnet:8.0  #cambia image, per usare aspnet.core runtime che è molto piu leggero
WORKDIR /app  #la directory di lavoro diventa /app

COPY --from=build /app/publish .  #/app/publish viene copiato in /app

ENV ASPNETCORE_URLS=http://+:8080  #dice a asp.net core di ascoltare su tutte le interfaccie sulla porta 8080. '+' significa 0.0.0.0 cioe accessibilità da fuori!quindi non devi settre esplicitamente nel code program.cs  builder.WebHost.UseUrls("http://0.0.0.0:8080");

EXPOSE 8080  #dice a docker che questa app usa la porta 8080!

ENTRYPOINT ["dotnet", "MyPrj.API.dll"]  #questo è il comando che parte quando il container si avvia.

#quindi quando deploy DEVI IMPOSTARE port: 8080, altrimenti il reverse proxy non trova l'api.

