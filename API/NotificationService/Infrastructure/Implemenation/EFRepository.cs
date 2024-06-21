using Application.Interfaces;
using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implemenation
{
    public abstract class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dataContext;
        public EFRepository(AppDbContext dataContext)
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
