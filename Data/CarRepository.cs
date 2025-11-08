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

    //Pour ajouter plusieurs voitures
    public void AddCars(List<Car> cars)
    {
        _carDbContext.Cars.AddRange(cars);
        _carDbContext.SaveChanges();
    }
    
    
    public List<Car> GetAllCar()
    {
        return _carDbContext.Cars.ToList();
    }
    
    public void UpdateCars(IEnumerable<Car> cars)
    {
        _carDbContext.Cars.UpdateRange(cars);
        _carDbContext.SaveChanges();
    }
    
    public void SellingCar()
    {
        Console.WriteLine("===== 🚗 Vente d'une voiture =====");
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

    
}
    