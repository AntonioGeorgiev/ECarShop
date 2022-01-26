using ECarShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECarShop.DL.Interfaces
{
    public interface IDealerRepository
    {
        Dealer Create(Dealer dealer);

        Dealer Update(Dealer dealer);

        Dealer Delete(int id);

        Dealer GetById(int id);

        IEnumerable<Dealer> GetAll();
    }
}
