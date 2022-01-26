using ECarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECarShop.Models.DTO
{
    public class Client
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public double Balance { get; set; }
        public PaymentType PaymentType  { get; set; }
    }
}
