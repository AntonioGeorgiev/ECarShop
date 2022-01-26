using ECarShop.Models.DTO;
using System;
using System.Collections.Generic;

namespace ECarShop.DL.InMemoryDb
{
    public static class CarInMemoryCollection
    {
        public static List<Car> CarDb = new List<Car>()
        {
            new Car()
            {
                Id = 1,
                Brand = "Opel",
                Model = "Vectra",
                Price = 5000,
                
            },
            new Car()
            {
                Id = 2,
                Brand = "BMW",
                Model = "E60",
                Price = 10000,
            },
            new Car()
            {
                Id = 3,
                Brand = "Mercedes Benz",
                Model = "S500",
                Price = 15000,
            },
            

        };
    }
}