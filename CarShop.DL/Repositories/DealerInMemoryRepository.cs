using ECarShop.DL.InMemoryDb;
using ECarShop.DL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ECarShop.Models.DTO;

namespace ECarShop.DL.Repositories
{
    public class DealerInMemoryRepository : IDealerRepository
    {
        public DealerInMemoryRepository()
        {

        }
        public Dealer Create(Dealer dealer)
        {
            DealerInMemoryCollection.DealerDB.Add(dealer);

            return dealer;
        }

        public Dealer Delete(int id)
        {
            var dealer = DealerInMemoryCollection.DealerDB.FirstOrDefault(dealer => dealer.Id == id);

            DealerInMemoryCollection.DealerDB.Remove(dealer);

            return dealer;
        }

        public IEnumerable<Dealer> GetAll()
        {
            return DealerInMemoryCollection.DealerDB;
        }

        public Dealer GetById(int id)
        {
            return DealerInMemoryCollection.DealerDB.FirstOrDefault(b => b.Id == id);
        }

        public Dealer Update(Dealer dealer)
        {
            var result = DealerInMemoryCollection.DealerDB.FirstOrDefault(b => b.Id == dealer.Id);

            result.Id = dealer.Id;
            result.Name = dealer.Name;
            result.PhoneNumber = dealer.PhoneNumber;
            

            return result;
        }
    }
}
