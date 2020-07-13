using System.Linq;
using Microsoft.EntityFrameworkCore;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;

namespace Taiga.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly TaigaContext _context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(TaigaContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            _context.Add(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual void Update(TEntity obj)
        {
            dbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            dbSet.Remove(dbSet.Find(id));
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }
    }
}
