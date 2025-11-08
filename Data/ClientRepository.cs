using Projet_Finale.Model;
using Projet_Finale.Data.InterfaceRepository;
using System.Collections.Generic;
using System.Linq;
using Projet_Finale.Utils;

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
        
        public void AddClient()
        {
            Console.WriteLine("=====  Ajouter un nouveau client =====");

            Console.WriteLine("Prénom : ");
            var firstName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine(" Le prénom est obligatoire.");
                return;
            }

            Console.WriteLine("Nom : ");
            var lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine(" Le nom est obligatoire.");
                return;
            }

            Console.WriteLine("Date de naissance (yyyy-MM-dd) : ");
            var birthInput = Console.ReadLine();
            if (!DateTime.TryParse(birthInput, out DateTime birthDate))
            {
                Console.WriteLine(" Date invalide.");
                return;
            }
            birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

            Console.WriteLine("Numéro de téléphone : ");
            var phone = Console.ReadLine()?.Trim();

            Console.WriteLine("Email : ");
            var email = Console.ReadLine()?.Trim();

            // Créer le client
            var client = new Client
            {
                id_client = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                PhoneNumber = phone,
                Email = email
            };

            // Ajouter à la DB
            _context.Customers.Add(client);
            _context.SaveChanges();

            Console.WriteLine($" Client ajouté avec succès : {client.FirstName} {client.LastName} (ID = {client.id_client})");
        }
    }

}