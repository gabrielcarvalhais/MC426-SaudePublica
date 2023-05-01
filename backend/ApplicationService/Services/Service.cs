using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Backend.Domain.Interfaces.Services;

namespace MC426_Backend.ApplicationService.Services
{
    public class Service<T> : IService<T> where T : class
    {
        protected IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual void Insert(T obj)
        {
            _repository.Insert(obj);
        }

        public virtual void Update(T obj)
        {
            _repository.Update(obj);
        }

        public virtual void Delete(T obj)
        {
            _repository.Delete(obj);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
