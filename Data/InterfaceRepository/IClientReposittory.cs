using Projet_Finale.Model;
using System.Collections.Generic;
namespace Projet_Finale.Data.InterfaceRepository;

public interface IClientRepository
{
    // pas utiliser car l'insertion est faite donc il est commenter dans le program.cs
    void AddClients(List<Client> clients);  // pour plusieurs
    // Ajouter le CSV CLient
    List<Client> GetAllClients();
    // Ajouter un(e) client(e)
    void AddClient();
}