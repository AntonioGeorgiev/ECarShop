using ECarShop.Models.DTO;
using ECarShop.Models.Enums;
using System;
using System.Collections.Generic;

namespace ECarShop.DL.InMemoryDb 
{ 
    public static class ClientInMemoryCollection
    {
        public static List<Client> ClientDb = new List<Client>
        {
            new Client()
            {
                Id = 1,
                Username = "Client1",
                Balance = 13000,
                PaymentType = Models.Enums.PaymentType.CreditCard,
            },
            new Client()
            {
                Id = 2,
                Username = "Client2",
                Balance = 17000,
                PaymentType = Models.Enums.PaymentType.Cash,
            },
            new Client()
            {
                Id = 3,
                Username = "Client3",
                Balance = 8000,
                PaymentType = Models.Enums.PaymentType.Cash,
            }
        };
    }
}