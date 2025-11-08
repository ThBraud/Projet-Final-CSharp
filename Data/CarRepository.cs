using Microsoft.EntityFrameworkCore;
using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Model;
using Projet_Finale.Data.InterfaceRepository;
namespace Projet_Finale.Data;
using System.Linq;
using System.Collections.Generic;


public class CarRepository : ICarRepository
{
    private readonly CarDbContext _carDbContext;

    public CarRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    //Pour ajouter le CSV voitures
    public void AddCars(List<Car> cars)
    {
        _carDbContext.Cars.AddRange(cars);
        _carDbContext.SaveChanges();
    }
    
    
    public List<Car> GetAllCar()
    {
        return _carDbContext.Cars.ToList();
    }
    
    // Pour lier les voitures et les clients
    public void UpdateCars(IEnumerable<Car> cars)
    {
        _carDbContext.Cars.UpdateRange(cars);
        _carDbContext.SaveChanges();
    }

    #region Vente de voiture

    //Pour vendre une voiture
    public void SellingCar()
    {
        Console.WriteLine("=====  Vente d'une voiture =====");
        Console.Write("Entrez l'ID de la voiture à vendre : ");
        var input = Console.ReadLine();

        // Vérification de l'entrée
        if (!Guid.TryParse(input, out Guid id))
        {
            Console.WriteLine(" ID invalide. Veuillez entrer un nombre entier.");
            return;
        }

        // Vérifie si la voiture existe et n’est pas encore vendue
        var car = _carDbContext.Cars
            .FirstOrDefault(c => c.Id_car == id && !c.IsSelling);

        if (car == null)
        {
            Console.WriteLine($" Aucune voiture trouvée avec l'ID {id}, ou elle est déjà vendue.");
            return;
        }

        // Marquer la voiture comme vendue
        car.IsSelling = true;

        // Sélection d’un client aléatoire si disponible
        var allClients = _carDbContext.Customers.ToList();
        Client? randomClient = null;
        if (allClients.Any())
        {
            var random = new Random();
            randomClient = allClients[random.Next(allClients.Count)];
            car.id_client = randomClient.id_client;
        }

        // Enregistrement en base
        _carDbContext.SaveChanges();

        // Affichage console
        Console.OutputEncoding = System.Text.Encoding.UTF8; // pour le symbole €
        Console.WriteLine("\n=====  Nouvelle vente =====");
        Console.WriteLine($"Voiture vendue : {car.Brand} {car.Model} ({car.Years}) | Couleur : {car.Color}");
        Console.WriteLine($"Prix TTC : {car.PriceIncludingTax} €");

        if (randomClient != null)
            Console.WriteLine($"Client : {randomClient.FirstName} {randomClient.LastName}");
        else
            Console.WriteLine("Client : Aucun client attribué.");

        Console.WriteLine(" Vente enregistrée en base de données.\n");
    }
    #endregion
    
    #region ajout voiture
    // Ajouter une voiture
    public void AddCar()
    {
        Console.WriteLine("=====  Ajouter une nouvelle voiture =====");

        Console.Write("Marque : ");
        var brand = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(brand))
        {
            Console.WriteLine("La marque est obligatoire.");
            return;
        }

        Console.Write("Modèle : ");
        var model = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(model))
        {
            Console.WriteLine(" Le modèle est obligatoire.");
            return;
        }

        Console.Write("Année : ");
        var yearInput = Console.ReadLine();
        if (!int.TryParse(yearInput, out int years))
        {
            Console.WriteLine(" Année invalide.");
            return;
        }

        Console.Write("Prix HT : ");
        var priceInput = Console.ReadLine();
        if (!float.TryParse(priceInput, System.Globalization.CultureInfo.InvariantCulture, out float preTaxPrice))
        {
            Console.WriteLine(" Prix invalide.");
            return;
        }

        Console.Write("Couleur : ");
        var color = Console.ReadLine()?.Trim();

        Console.Write("Vendu ? (true/false) : ");
        var isSellingInput = Console.ReadLine();
        bool isSelling = false;
        if (!string.IsNullOrWhiteSpace(isSellingInput))
            bool.TryParse(isSellingInput, out isSelling);

        // Crée la voiture avec un GUID unique
        var car = new Car
        {
            Id_car = Guid.NewGuid(),
            Brand = brand,
            Model = model,
            Years = years,
            PreTaxPrices = preTaxPrice,
            PriceIncludingTax = preTaxPrice * 1.2f,
            Color = color,
            IsSelling = isSelling,
            id_client = null // Pas de client par défaut
        };

        _carDbContext.Cars.Add(car);
        _carDbContext.SaveChanges();

        Console.WriteLine($"\n Voiture ajoutée avec succès : {car.Brand} {car.Model} ({car.Years})");
        Console.WriteLine($"ID de la voiture : {car.Id_car}");
    }
    #endregion
    
}
    