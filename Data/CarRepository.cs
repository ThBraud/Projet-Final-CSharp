using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Model;

namespace Projet_Finale.Data;

public class CarRepository : ICarRepository
{
    private readonly CarDbContext _carDbContext;

    public CarRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    public List<Car> GetAllCar()
    {
        return _carDbContext.Cars.ToList();
    }
}
    