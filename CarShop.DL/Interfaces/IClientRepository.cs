using System;
using System.Collections.Generic;
using ECarShop.Models.DTO;

namespace ECarShop.DL.Interfaces
{
    public interface IClientRepository
    {
        Client Create(Client client);

        Client Update(Client client);

        Client Delete(int id);

        Client GetById(int id);

        IEnumerable<Client> GetAll();
    }
}
