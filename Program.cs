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
        
        services.AddTransient<IClientRepository, clientRepository>();
    })
    .Build();

using var scope = host.Services.CreateScope();
IClientRepository clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository>();

#endregion