using OnlineBankingApp.DataLayer.Abstract;
using OnlineBankingApp.DataLayer.Concrete;
using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly BankingContext _context;

        public GenericRepository(BankingContext context)
        {
            _context = context;
        }

        public async Task<Result> Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            if (value is not null)
            {
                _context.Set<T>().Remove(value);
                await _context.SaveChangesAsync();
                return new Result { Success = true };
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

        public async Task<List<T>> GetAll()
        {
            var values = _context.Set<T>().ToList();
            return values;
        }

        public async Task<T> GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            return value;
        }

        public async Task Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
