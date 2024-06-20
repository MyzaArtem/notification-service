using Microsoft.EntityFrameworkCore;
using NotificationService.Abstractions;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public abstract class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dataContext;
        public EFRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity2 = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity2 == null)
            {
                return;
            }
            _dataContext.Set<T>().Remove(entity2);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            var entity2 = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (entity2 == null)
            {
                return;
            }
            entity2 = entity;
            await _dataContext.SaveChangesAsync();
        }
    }
}
