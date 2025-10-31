using Projet_Finale.Data.InterfaceRepository;
using Projet_Finale.Model;

namespace Projet_Finale.Data;

public class clientRepository : IClientRepository
{
    private readonly CarDbContext _carDbContext;

    public clientRepository(CarDbContext carDbContext)
    {
        _carDbContext = carDbContext;
    }

    public List<Client> GetAllClient()
    {
        return _carDbContext.Customers.ToList();
    }
}
    