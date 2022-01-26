using ECarShop.Models.DTO;
using ECarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECarShop.Models.Responses
{
   public class CarResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }



    }
}
