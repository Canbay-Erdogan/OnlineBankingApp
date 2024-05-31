using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.BusinessLayer.Abstract
{
    public interface IAccountService:IGenericService<Account>
    {
        Task Deposit(AccountBalanceOperationDto entity);
        Task Withdraw(AccountBalanceOperationDto entity);
    }
}
