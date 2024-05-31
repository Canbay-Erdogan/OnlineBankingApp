using FluentValidation;
using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.BusinessLayer.Validations
{
    public class UserValidator : AbstractValidator<RegisterUserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Username).NotEmpty();

            RuleFor(u => u.Password).MinimumLength(8).WithMessage("'{Password}' length must be more than 8 characters").
                Matches("[A-Z]+").WithMessage("'{Password}' must contain one or more capital letters.")
               .Matches("[a-z]+").WithMessage("'{Password}' must contain one or more lowercase letters.")
               .Matches(@"(\d)+").WithMessage("'{Password}' must contain one or more digits.")
               .Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage("'{ Password}' must contain one or more special characters.");
        }
    }
}
