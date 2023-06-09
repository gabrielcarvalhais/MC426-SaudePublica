namespace MC426_Backend.Domain.Interfaces.Services
{
    public interface IService<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
