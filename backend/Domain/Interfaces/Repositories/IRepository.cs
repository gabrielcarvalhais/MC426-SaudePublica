namespace MC426_Backend.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
