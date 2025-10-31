using Projet_Finale.Model;
using Projet_Finale.Data.InterfaceRepository;
using System.Collections.Generic;
using System.Linq;

namespace Projet_Finale.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly CarDbContext _context;

        public ClientRepository(CarDbContext context)
        {
            _context = context;
        }

        public void AddClients(List<Client> clients)
        {
            _context.Customers.AddRange(clients); // ajoute toute la liste en une seule fois
            _context.SaveChanges();             // enregistre les changements en DB
        }
        
        public List<Client> GetAllClients()
        {
            return _context.Customers.ToList(); // récupérer toutes les lignes
        }
    }
}