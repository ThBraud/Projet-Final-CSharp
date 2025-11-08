using Projet_Finale.Model;

namespace Projet_Finale.Data.InterfaceRepository;

public interface ICarRepository
{
    // pas utiliser car l'insertion est faite donc il est commenter dans le program.cs
    // Ajouter le CSV voitures
    void AddCars(List<Car> cars); 
    List<Car> GetAllCar();
       // pas utiliser car l'insertion est faite donc il est commenter dans le program.cs
    // Pour lier les voitures et les clients
       void UpdateCars(IEnumerable<Car> cars);
    // Pour vendre une voiture
    void SellingCar();
    // Pour ajouter une voiture
    void AddCar();
}
