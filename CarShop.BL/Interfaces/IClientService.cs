using ECarShop.Models.DTO;
using System.Collections.Generic;

namespace BarManager.BL.Interfaces
{
    public interface IClientService
    {
        Client Create(Client client);

        Client Update(Client client);

        Client Delete(int id);

        Client GetById(int id);

        IEnumerable<Client> GetAll();
    }
}
