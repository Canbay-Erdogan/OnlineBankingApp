using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.DataLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task<Result> Delete(int id);
        Task<T> GetById(int id);
        Task< List<T>> GetAll();
    }
}
