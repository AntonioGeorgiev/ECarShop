using ECarShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECarShop.DL.InMemoryDb
{
    public class DealerInMemoryCollection
    {
        public static List<Dealer> DealerDB = new List<Dealer>()
        {
            new Dealer()
            {
                Id = 1,
                Name = "Dealer1",
                PhoneNumber = 696969
            },
            new Dealer()
            {
                Id = 2,
                Name = "Dealer2",
                PhoneNumber = 123456
            },
            new Dealer()
            {
                Id = 3,
                Name = "Dealer3",
                PhoneNumber = 875600
            }




        };

    }
}

