# Projet Final de C#
## 🧭 Contexte  
Vous travaillez pour un **concessionnaire automobile** qui souhaite un outil pour **gérer son parc de véhicules**.  
L’objectif est de pouvoir **identifier** si une voiture est **vendue** ou **en vente**, tout en garantissant qu’une voiture vendue ait **un client associé**.  

---

## 🎯 Objectif du projet  
Créer une **application en C# (.NET 9)** permettant de :  
- Importer des données depuis des **fichiers CSV** (clients et voitures) ;  
- Générer les **tables correspondantes** dans une base de données **PostgreSQL**
- Gérer la **relation** entre les **clients** et les **voitures**.  


## Extensions a installer 

```bash
dotnet add package Microsoft.EntityFrameworkCore  
dotnet add package Microsoft.Extensions.Configuration  
dotnet add package Microsoft.Extensions.Configuration.Json  
dotnet add package Microsoft.Extensions.Hosting  
dotnet add package Microsoft.Extensions.DependencyInjection  
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```
