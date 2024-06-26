using Application.Interfaces;
using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Implemenation
{
    public class EFRepository<T> : IRepository<T>, IQuerier<T> where T : BaseEntity
    {
        private readonly AppDbContext _dataContext;
        public EFRepository(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }
        public IQueryable<T> Query()
        {
            return _dataContext.Set<T>();
        }

        public async Task DeleteAsync(Guid id)
        {
            /*var entity2 = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity2 == null)
            {
                return;
            }
            _dataContext.Set<T>().Remove(entity2);
            await _dataContext.SaveChangesAsync();*/
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }
            entity.Status = (short)Status.Deleted;
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
