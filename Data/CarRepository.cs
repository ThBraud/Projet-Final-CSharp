using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Model;
using Projet_Finale.Data.InterfaceRepository;
namespace Projet_Finale.Data;

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
    
    
    
}
    