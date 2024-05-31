using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.DataLayer.Abstract
{
    public interface IAccountDal:IGenericDal<Account>
    {
        Task Deposit(AccountBalanceOperationDto entity);
        Task Withdraw(AccountBalanceOperationDto entity);
    }
}
