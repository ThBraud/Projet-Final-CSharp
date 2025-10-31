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

    public void Addcars(Car cars)
    {
        _carDbContext.Cars.Add(cars);

        _carDbContext.SaveChanges();
    }
}