using ECarShop.Models.DTO;
using ECarShop.Models.Enums;
using System.Collections.Generic;

namespace ECarShop.Models.Requests
{
    public class CarRequest
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        
    }
}
