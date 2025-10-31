using Projet_Finale.Model;

namespace Projet_Finale.Data.InterfaceRepository;

public interface IClientRepository
{
    List<Client> GetAllClient();
}