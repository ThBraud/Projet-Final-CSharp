using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Data;
using Projet_Finale.Model;
using Projet_Finale.Utils;

#region lancement services

// Charger la configuration manuellement
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\Csharp_Projet_Finale\Projet_Finale\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<CarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // On enregistre notre service applicatif
        services.AddTransient<DbConnection>();
        
        services.AddTransient<ICarRepository, CarRepository>();
    })
    .Build();

using var scope = host.Services.CreateScope();
ICarRepository clientRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();

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
    car.PreTaxPrices= float.Parse(line.Split('/')[3]);
    car.PriceIncludingTax = car.PreTaxPrices * 1.2f;
    car.Color = line.Split('/')[4];
    car.IsSelling = bool.Parse(line.Split('/')[5]);
    
    cars.Add(car);
}

#endregion


#region CSV Client

String path_client = configuration.GetRequiredSection("CSVFiles2")["Customer"];

List<Client> client = new List<Client>(); 

var lignes_client = File.ReadAllLines(path_client);

for (int i = 1; i < lignes_client.Length; i++)
{
    String line = lignes_client[i];
    Client c = new Client();
    c.LastName = line.Split('%')[0];
    c.FirstName = line.Split('%')[1];
    c.BirthDate = DateTimeUtils.ConvertToDateTime(line.Split(',')[3]);
    c.PhoneNumber = line.Split('%')[3];
    c.Email = line.Split('%')[4];
    
    client.Add(c);
    
    
}

#endregion