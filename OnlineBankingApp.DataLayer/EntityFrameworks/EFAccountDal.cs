using OnlineBankingApp.DataLayer.Abstract;
using OnlineBankingApp.DataLayer.Concrete;
using OnlineBankingApp.DataLayer.Repositories;
using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.DataLayer.EntityFrameworks
{
    public class EFAccountDal : GenericRepository<Account>, IAccountDal
    {
        private readonly BankingContext _context;
        public EFAccountDal(BankingContext context) : base(context)
        {
            _context = context;
        }

        public async Task Deposit(AccountBalanceOperationDto entity)
        {
            var value = await _context.Accounts.FindAsync(entity.Id);
            if (value is not null)
            {
                value.Balance += entity.Amount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Withdraw(AccountBalanceOperationDto entity)
        {
            var value = await _context.Accounts.FindAsync(entity.Id);
            if (value is not null && value.Balance >= entity.Amount)
            {
                value.Balance -= entity.Amount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
