using ECarShop.Models.Enums;


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
