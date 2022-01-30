using ECarShop.Models.Enums;

namespace ECarShop.Models.Requests

{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public double Balance { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
