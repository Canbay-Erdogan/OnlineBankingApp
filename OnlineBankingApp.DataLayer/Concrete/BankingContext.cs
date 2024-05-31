using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.DataLayer.Concrete
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> opt):base(opt) 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = OnlineBankingDB;");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }  

    }
}

