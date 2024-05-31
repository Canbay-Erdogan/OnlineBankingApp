using OnlineBankingApp.DataLayer.Abstract;
using OnlineBankingApp.DataLayer.Concrete;
using OnlineBankingApp.DataLayer.Repositories;
using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.DataLayer.EntityFrameworks
{
    public class EFUserDal : GenericRepository<User>, IUserDal
    {
        public EFUserDal(BankingContext context) : base(context)
        {
        }
    }
}
