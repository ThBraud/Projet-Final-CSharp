using Projet_Finale.Model;
namespace Projet_Finale.Data; 
using Npgsql;

public class DbConnection
{
    private readonly CarDbContext _carDbContext;

    public DbConnection(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }
    
    // Ajouter le CSV voitures
    public void AddCars(List<Car> cars)
    {
        _carDbContext.Cars.AddRange(cars);

        _carDbContext.SaveChanges();
    }
    // Ajouter le CSV client
    public void AddClients(List<Client> clients)
    {
        _carDbContext.Customers.AddRange(clients);
        _carDbContext.SaveChanges();
    }
}