using ECarShop.DL.InMemoryDb;
using ECarShop.DL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.DL.Repositories
{
    public class ClientInMemoryRepository : IClientRepository
    {
        public ClientInMemoryRepository()
        {

        }
        public Client Create(Client client)
        {
            ClientInMemoryCollection.ClientDb.Add(client);

            return client;
        }

        public Client Delete(int id)
        {
            var client = ClientInMemoryCollection.ClientDb.FirstOrDefault(client => client.Id == id);

            ClientInMemoryCollection.ClientDb.Remove(client);

            return client;
        }

        public IEnumerable<Client> GetAll()
        {
            return ClientInMemoryCollection.ClientDb;
        }

        public Client GetById(int id)
        {
            return ClientInMemoryCollection.ClientDb.FirstOrDefault(b => b.Id == id);
        }

        public Client Update(Client client)
        {
            var result = ClientInMemoryCollection.ClientDb.FirstOrDefault(b => b.Id == client.Id);

            result.Id = client.Id;
            result.Username = client.Username;
            result.Balance = client.Balance;
            result.PaymentType = client.PaymentType;


            return result;
        }
    }
}

