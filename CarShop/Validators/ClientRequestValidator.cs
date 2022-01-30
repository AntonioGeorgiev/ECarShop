using FluentValidation;
using ECarShop.Models.Requests;
namespace ECarShop.Validators
{
    public class ClientRequestValidator: AbstractValidator<ClientRequest>
    {
        public ClientRequestValidator()
        {
            RuleFor(r => r.Username).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(b => b.Balance).NotNull().GreaterThan(1);
        }
    }
}
