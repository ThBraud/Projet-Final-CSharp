using Projet_Finale.Model;
using System.Collections.Generic;
namespace Projet_Finale.Data.InterfaceRepository;

public interface IClientRepository
{
    void AddClients(List<Client> clients);  // pour plusieurs
    List<Client> GetAllClients();
}