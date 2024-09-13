using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventsWebApp.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected EventsDbContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(
            EventsDbContext context,
            ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public abstract Task<bool> Delete(Guid id);

        public abstract Task<IEnumerable<T>> All();

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public abstract Task<bool> Upsert(T entity);
    }
}
