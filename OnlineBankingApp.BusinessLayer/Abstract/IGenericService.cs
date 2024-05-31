using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TInsert(T entity);
        Task TUpdate(T entity);
        Task<Result> TDelete(int id);
        Task<T> TGetById(int id);
        Task<List<T>> TGetAll();
    }
}