using ECarShop.Models.Requests;
using FluentValidation;

namespace CarShop.Validators
{
    public class CarRequestValidator : AbstractValidator<CarRequest>
    {
        public CarRequestValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().NotNull();
            RuleFor(x => x.Model).NotEmpty().NotNull();
        }

    }
}