#region Using Directives
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Data;
using Projet_Finale.Model;
using Projet_Finale.Utils;
using System.Globalization;
using Microsoft.Extensions.Logging;
#endregion

#region lancement services

// Charger la configuration manuellement
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\Csharp_Projet_Finale\Projet_Finale\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    //Ajouter avec l'aide de l'IA pour avoir une meilleure visibilité sur la console, au moment de l'appel de certain input. 
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders(); // supprime la sortie console par défaut
        logging.SetMinimumLevel(LogLevel.Warning); // affiche seulement les warnings et erreurs
    })
    
    .ConfigureServices(services =>
    {
        services.AddDbContext<CarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            

        // On enregistre notre service applicatif
        services.AddTransient<DbConnection>();
        
        services.AddTransient<ICarRepository, CarRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var carRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();
using var scope_client = host.Services.CreateScope();
IClientRepository clientRepository = scope_client.ServiceProvider.GetRequiredService<IClientRepository>();

    #endregion

#region CSV Client

String path_client = configuration.GetRequiredSection("CSVFiles2")["Customers"];

List<Client> client = new List<Client>(); 

var lignes_client = File.ReadAllLines(path_client);

for (int i = 1; i < lignes_client.Length; i++)
{
    String line = lignes_client[i];
    Client c = new Client();
    c.LastName = line.Split('%')[0];
    c.FirstName = line.Split('%')[1];
    c.BirthDate = DateTimeUtils.ConvertToDateTime(line.Split('%')[2]);
    c.PhoneNumber = line.Split('%')[3];
    c.Email = line.Split('%')[4];
    
    client.Add(c);
    
    
}
//Insertion données Client
// Commenter pour pas refaire l'insertion a chaque fois. 
//clientRepository.AddClients(client);
var clientsFromDb = clientRepository.GetAllClients();
#endregion



#region  CSV Car

String path_car = configuration.GetRequiredSection("CSVFiles")["Car"];

List<Car> cars = new List<Car>(); 

var lignes = File.ReadAllLines(path_car);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Car car = new Car();
    car.Brand = line.Split('/')[0];
    car.Model = line.Split('/')[1];
    car.Years = int.Parse(line.Split('/')[2]);
    car.PreTaxPrices = float.Parse(line.Split('/')[3], CultureInfo.InvariantCulture);
    car.PriceIncludingTax = car.PreTaxPrices * 1.2f;
    car.Color = line.Split('/')[4];
    car.IsSelling = bool.Parse(line.Split('/')[5]);

    cars.Add(car);
    
}

//Insertion données Car
    // Commenter pour pas refaire l'insertion a chaque fois. 
    //carRepository.AddCars(cars);

// Après l'insertion des voitures
    var carsFromDb = carRepository.GetAllCar();
    var clients = clientRepository.GetAllClients().ToList();
    var random = new Random();

    // On récupère les clients déjà présents dans la BDD
    var allClients = clientRepository.GetAllClients().ToList();

// On parcourt les voitures pour assigner un client aléatoire si IsSelling = true
    foreach (var car in carsFromDb)
    {
        if (car.IsSelling)
        {
            // Choisit un client aléatoire
            var randomClient = allClients[random.Next(allClients.Count)];
        
            // Lie la voiture au client
            car.id_client = randomClient.id_client;
        }
        else
        {
            // Voitures non vendues → pas de client
            car.id_client = null;
        }
    }

// Mise à jour en base des voitures avec leur ClientId
    // Commenter pour pas refaire l'insertion a chaque fois. 
    //carRepository.UpdateCars(carsFromDb);
    #endregion

# region Liste voitures
// Pour avoir le symbole € (donner par chatgpt)
    Console.OutputEncoding = System.Text.Encoding.UTF8;
// avoir la liste de toutes les voitures
    
    var allCars = carRepository.GetAllCar();

    Console.WriteLine("\n===== Liste complète des voitures =====\n");
    foreach (var car in allCars)
    {
        Console.WriteLine($"Marque : {car.Brand} | Modèle : {car.Model} | Couleur : {car.Color} | Année : {car.Years} | Prix TTC : {car.PriceIncludingTax} €");
    }

#endregion

#region new car and new customer
// question permetant de lancer le projet sans rajouter une voiture a chaque fois
Console.WriteLine("Voulez vous ajouter une nouvelle voiture? (oui/ non) : ");
var newcar = Console.ReadLine();
if (newcar == "oui")
{
    using var NewCar = host.Services.CreateScope();
    var AddNewCar = NewCar.ServiceProvider.GetRequiredService<ICarRepository>();
    AddNewCar.AddCar();
}
if (newcar == "non"){ }
else
{
    Console.WriteLine($"répondez par oui ou par non,  pas avec {newcar}\"");
}
Console.WriteLine("Voulez vous ajouter un nouveau client? (oui / non) : ");
var newclient = Console.ReadLine();
if (newclient == "oui")
{
    using var NewClient = host.Services.CreateScope();
    var NewCustomer = NewClient.ServiceProvider.GetRequiredService<IClientRepository>();
    NewCustomer.AddClient();
}
if (newclient == "non"){ }
else
{
    Console.WriteLine($"répondez par oui ou par non, pas avec {newclient}");
}
#endregion

#region Sell car

Console.WriteLine("voulez vous acheter une voiture? (oui/ non) : ");
var newsell = Console.ReadLine();
if (newsell == "oui")
{
    using var selling = host.Services.CreateScope();
    var carService = selling.ServiceProvider.GetRequiredService<ICarRepository>();
    carService.SellingCar();
}

if (newsell == "non"){ }
else
{
    Console.WriteLine($"répondez par oui ou par non,  pas avec {newsell}\"");
}
#endregion


# region historique achat
// Pour créer un historique des achats
var purchaseHistory = new List<(Car car, Client client, DateTime purchaseDate)>();

foreach (var car in carsFromDb)
{
    if (car.IsSelling && car.id_client != null)
    {
        var purchaser = clientsFromDb.First(c => c.id_client == car.id_client);

        // Générer une date d'achat aléatoire dans les 5 dernières années, on se dit qu'on a ouvert il y a 5 ans
        var start = DateTime.Now.AddYears(-5);
        var range = (DateTime.Now - start).Days;
        var purchaseDate = start.AddDays(random.Next(range));

        purchaseHistory.Add((car, purchaser, purchaseDate));
    }
}

// Trier l'historique par date croissante
purchaseHistory = purchaseHistory.OrderBy(p => p.purchaseDate).ToList();

// Pour avoir le symbole € (donner par chatgpt)
Console.OutputEncoding = System.Text.Encoding.UTF8;
// afficher l'historique d'achat dans la console
Console.WriteLine("\n===== Historique des achats =====\n");

foreach (var entry in purchaseHistory)
{
    Console.WriteLine($"Marque: {entry.car.Brand} Model: {entry.car.Model} Years: {entry.car.Years} Price : {entry.car.PriceIncludingTax} € | " +
                      $"Date d'achat: {entry.purchaseDate.ToShortDateString()} | " +
                      $"Client: {entry.client.FirstName} {entry.client.LastName}");
}

#endregion
