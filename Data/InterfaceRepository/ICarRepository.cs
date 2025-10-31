using Projet_Finale.Model;

namespace Projet_Finale.Data.InterfaceRepository;

public interface ICarRepository
{
     
    void AddCars(List<Car> cars); 
    List<Car> GetAllCar();
       
}