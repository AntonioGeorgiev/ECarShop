using FluentValidation;
using ECarShop.Models.Requests;
namespace ECarShop.Validators
{
    public class DealerRequestValidator : AbstractValidator<DealerRequest>
    {
        public DealerRequestValidator()
        {
            RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(b => b.PhoneNumber).NotNull().NotEmpty().GreaterThan(5).LessThan(11);
        }
    }
}