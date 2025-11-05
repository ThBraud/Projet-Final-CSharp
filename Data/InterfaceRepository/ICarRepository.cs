using Projet_Finale.Model;

namespace Projet_Finale.Data.InterfaceRepository;

public interface ICarRepository
{
    // pas utiliser car l'insertion est faite donc il est commenter dans le program.cs
    void AddCars(List<Car> cars); 
    List<Car> GetAllCar();
       // pas utiliser car l'insertion est faite donc il est commenter dans le program.cs
    void UpdateCars(IEnumerable<Car> cars);
    void SellingCar();
}
