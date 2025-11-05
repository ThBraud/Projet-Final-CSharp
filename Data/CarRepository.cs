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
        // récupère une liste de voiture non vendu
        var availableCars =  _carDbContext.Cars
            .Where(ca => !ca.IsSelling)
            .Select(ca => ca.Id_car)
            .ToList();
        
        // choisi une voiture dans cette list
        var random =new Random();
        var randomCarId = availableCars[new Random().Next(availableCars.Count)];
        
        //récupération de la voiture
        var car =  _carDbContext.Cars.First(c => c.Id_car == randomCarId);
        
        // modification de la valeur en true
        car.IsSelling=true;
        
        _carDbContext.SaveChanges();
        
    }
    
}
    