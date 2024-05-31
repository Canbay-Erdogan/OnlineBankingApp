using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.BusinessLayer.Abstract;
using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.WebAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var values = await _accountService.TGetAll();
            if (values.Count > 0)
            {
                return Ok(values);
            }
            else
            {
                return BadRequest("No account has been created yet");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var value = await _accountService.TGetById(id);
            if (value is not null)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest("account not found");
            }

        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accountService.TInsert(account);
                return Ok(account.AccountHolderName + "'s account has been created " + DateTime.Now);
            }
            else
            {
                return BadRequest("account could not be created");
            }
        }

        [Route("deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit(AccountBalanceOperationDto balanceOperation)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var value = _accountService.TGetById(balanceOperation.Id);
                if (value is not null)
                {
                    //value.Result.Balance += balanceOperation.Amount;
                    await _accountService.Deposit(balanceOperation);
                    return Ok("deposit successful, current balance " + value.Result.Balance);
                }
                else
                {
                    return BadRequest("account not found");
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }



        }

        [Route("withdraw")]
        [HttpPost]
        public async Task<IActionResult> Withdraw(AccountBalanceOperationDto balanceOperation)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var value = _accountService.TGetById(balanceOperation.Id);
                if (value is not null && value.Result.Balance >= balanceOperation.Amount)
                {
                    //value.Result.Balance -= balanceOperation.Amount;
                    await _accountService.Withdraw(balanceOperation);
                    return Ok("Withdraw successful, current balance " + value.Result.Balance);
                }
                else if (value.Result.Balance >= balanceOperation.Amount)
                {
                    return BadRequest("Insufficient balance");
                }
                else
                {
                    return BadRequest("account not found");
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }


        }
    }
}
