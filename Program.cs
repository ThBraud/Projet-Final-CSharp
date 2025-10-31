using Projet_Finale;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Projet_Finale.Data.InterfaceRepository;
using Npgsql;
using Projet_Finale.Data;
using Projet_Finale.Model;

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

#region  CSV

String path = configuration.GetRequiredSection("CSVFiles")["CoursSupDeVinci"];

List<Car> cars = new List<Car>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Car person = new Car();
    cars.
    cars.brand = line.Split
    person.Lastname = line.Split(',')[1];
    person.Firstname = line.Split(',')[2];
    person.Birthdate = DateTimeUtils.ConvertToDateTime(line.Split(',')[3]);
    person.S = Int32.Parse(line.Split(',')[5]);
    
  
    
   
    
    cars.Add(person);
}

#endregion