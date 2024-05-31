using OnlineBankingApp.BusinessLayer.Abstract;
using OnlineBankingApp.BusinessLayer.MQService;
using OnlineBankingApp.DataLayer.Abstract;
using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;
using System.Security.Principal;

namespace OnlineBankingApp.BusinessLayer.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDal _accountDal;
        private readonly MessageQueueService _messageQueueService;

        public AccountManager(IAccountDal accountDal, MessageQueueService messageQueueService)
        {
            _accountDal = accountDal;
            _messageQueueService = messageQueueService;
        }

        public async Task<Result> TDelete(int id)
        {
            var value = _accountDal.GetById(id);
            if (value == null)
            {
                await _accountDal.Delete(id);
                return new Result
                {
                    Success = true,
                };
            }
            else
            {
                return new Result
                {
                    Success = false,
                    ErrorMessage = "object not found"
                };
            }

        }

        public async Task<List<Account>> TGetAll()
        {
            return await _accountDal.GetAll();
        }

        public async Task<Account> TGetById(int id)
        {
            return await _accountDal.GetById(id);
        }

        public async Task TInsert(Account entity)
        {
            await _accountDal.Insert(entity);
            _messageQueueService.PublishMessage($"Account created: {entity.AccountNumber}");
        }

        public async Task TUpdate(Account entity)
        {
            await _accountDal.Update(entity);
        }

        public async Task Deposit(AccountBalanceOperationDto balanceOperation)
        {
            var value = _accountDal.GetById(balanceOperation.Id);
            if (value is not null)
            {
                //value.Result.Balance += balanceOperation.Amount;
                await _accountDal.Deposit(balanceOperation);
                _messageQueueService.PublishMessage($"Deposited {balanceOperation.Amount} to account: {balanceOperation.Id}");
            }
        }

        public async Task Withdraw(AccountBalanceOperationDto balanceOperation)
        {
            var value = _accountDal.GetById(balanceOperation.Id);
            if (value is not null)
            {
                //value.Result.Balance -= balanceOperation.Amount;
                await _accountDal.Withdraw(balanceOperation);
                _messageQueueService.PublishMessage($"Withdrew {balanceOperation.Amount} from account: {balanceOperation.Id}");
            }
        }
    }
}
