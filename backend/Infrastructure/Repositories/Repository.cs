using MC426_Backend.Domain.Interfaces.Repositories;
using MC426_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MC426_Backend.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SaudePublicaContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(SaudePublicaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual void Insert(T obj)
        {
            _dbSet.Add(obj);

            _context.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            _dbSet.Update(obj);

            _context.SaveChanges();
        }

        public virtual void Delete(T obj)
        {
            _dbSet.Remove(obj);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
