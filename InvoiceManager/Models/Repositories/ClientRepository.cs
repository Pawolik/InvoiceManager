using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class ClientRepository
    {
        internal List<Client> GetClients(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public Client GetClient(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients.Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public void AddClient(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        public void UpdateClient(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToUpdate = context.Clients.Single(x => x.Id == client.Id && x.UserId == client.UserId);

                clientToUpdate.Name = client.Name;
                clientToUpdate.Email = client.Email;

                context.SaveChanges();
            }
        }

        public void DeleteClient(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToDelete = context.Clients.Single(x => x.Id == id && x.UserId == userId);
                context.Clients.Remove(clientToDelete);
                context.SaveChanges();
            }
        }
    }
}