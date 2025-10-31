using Projet_Finale.Model;
namespace Projet_Finale.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class CarDbContext : DbContext
{
    // --- Tables principales ---
    public DbSet<Client> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }
    
    // --- Constructeur ---
    public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
    { }
    // Constructeur vide pour EF CLI
    public CarDbContext() { }

    // --- Configuration des relations ---
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relation Clients -> Car (1..n)
        modelBuilder.Entity<Cars>()
            .HasOne(c => c.Id_car)
            .WithMany(d => d.id_client)
            .HasForeignKey(p => p.id_client);
    }

    // --- Configuration de la connexion ---
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Charger la configuration manuellement
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Csharp_Projet_Finale\Projet_Finale\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
