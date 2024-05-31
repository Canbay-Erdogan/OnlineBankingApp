using FluentValidation;
using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.BusinessLayer.Validations
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Balance).GreaterThanOrEqualTo(0).WithMessage("Bakiye negatif olamaz");
            RuleFor(a => a.AccountHolderName).MinimumLength(3).WithMessage("İsim 3 karekterden az olamaz");
        }
    }
}
