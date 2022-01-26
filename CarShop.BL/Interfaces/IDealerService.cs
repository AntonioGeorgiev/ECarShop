using ECarShop.Models.DTO;
using System.Collections.Generic;
namespace ECarShop.BL.Interfaces
{
    public interface IDealerService
    {
        Dealer Create(Dealer dealer);

        Dealer Update(Dealer dealer);

        Dealer Delete(int id);

        Dealer GetById(int id);

        IEnumerable<Dealer> GetAll();
    }
}
