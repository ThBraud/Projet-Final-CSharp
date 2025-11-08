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

# 🚀 Toutes les informations pour lancer le projet  
## 📥 Cloner le projet 
Pour commencer il vous faudra cloner ce repository avec la commande suivante ``git clone https://github.com/ThBraud/Projet-Final-CSharp.git``

## 🛠️ Applications à installer  
Il vous faudra installer les deux applications suivantes : [Rider JetBrains](https://www.jetbrains.com/fr-fr/rider/download/?section=windows), un IDE parfait pour le C# et [PostGreeSQL](https://www.postgresql.org/download), pour créer des bases de données.  

## 🖥️ Ouvrir le projet sur Rider et installer les extensions requises.

Après avoir ouvert le projet sur Rider, il faut ouvrir le terminal dans Rider directement. C'est le 3ème onglet en bas à gauche en partant du bas.  
Dans ce terminal copier-coller les extensions suivantes (vous pouvez tout copier d'un coup, le terminal les exécutera un par un) : 

```bash
dotnet add package Microsoft.EntityFrameworkCore  
dotnet add package Microsoft.Extensions.Configuration  
dotnet add package Microsoft.Extensions.Configuration.Json  
dotnet add package Microsoft.Extensions.Hosting  
dotnet add package Microsoft.Extensions.DependencyInjection  
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
```  

## 🗄️ Création d'une database  
Après avoir installé PostgreSQL, vous devriez avoir sur votre machine PgAdmin 4. Ouvrez-le et créez une database, pour cela il suffit de faire un clic droit sur Database puis Create. Une fois cela fait, laisser PgAdmin ouvert en fond.  
Puis sur Rider dans le projet, vous devez modifier le fichier appsettings.json. Précisemment cette ligne ``"DefaultConnection": "Host=localhost;Port=5432;Database=Projet;Username=postgres;Password=root"``.  
Les paramètres à changer sont le port si au démarrage de pgAdmin, vous avez modifié le port par défaut. Le Nom de votre Database, votre Username et votre Password liés à votre PgAdmin.  
Une fois cela fait, sur Rider dans les onglets sur la droite, cliquer sur Database (le 3ème onglet). Puis connecter votre Database au projet. Sélectionnez bien PostgreSQL. En cas d'erreur, cela peut être lié à la méthode d'authentification, dans ce cas, cliquez sur l'erreur et vous pourrez la modifier.  

## 🔄 Migration pour créer les tables liée au class C#
La prochaine étape consiste à faire une migration pour créer sur votre database les tables liées aux classes du projet. Pour cela, rouvrez le terminal et exécutez les commandes suivantes :  
```bash
dotnet ef migrations add NomDeTaMigration
dotnet ef database update
```

## 📊 Insérer les données des CSV dans votre Database
Pour insérer les données des CSV dans votre Database, tout est dans le program.cs. Il vous suffit d'enlever les commentaires présents au niveau des insertions.  
Pour le CSV client : Dans la région CSV client, **ligne 75**.  
Pour le CSV car : Dans la région CSV voitures, **ligne 107 et 137**. Dans le cas du CSV voiture, il n'y en a pas vraiment deux, simplement le premier insère les données des voitures et le deuxième sert à faire le lien entre les clients et les voitures. 

>[!Warning] 
N'oubliez pas de remettre les commentaires, une fois l'insertion faite. Sinon les données seront réinsérer à chaque fois que le projet sera lancé. 


# ▶️ Lancement du projet 
Pour lancer le projet, vous pouvez le lancer avec le bouton run de Rider ou alors avec ``dotnet run``