using ECarShop.Models.Enums;

namespace ECarShop.Models.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public double Balance { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
