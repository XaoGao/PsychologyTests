using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _dbSet;
        private IMemoryCache cache;
        private const int CAHSE_TIME_LIFE_IN_MINUT = 15;
        public GenericRepository(DataContext context, IMemoryCache cache)
        {
            this.cache = cache;
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await EntityFrameworkQueryableExtensions.ToListAsync(_dbSet.AsNoTracking());

            return entities;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            TEntity entity = null;

            string key = id + "-" + entity.GetType();
                
            if(!cache.TryGetValue(key, out entity))
            {
                entity = await _dbSet.FindAsync(id);
                if(entity != null)
                    cache.Set(key, entity, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CAHSE_TIME_LIFE_IN_MINUT)));
            }

            return entity;
        }

        public IEnumerable<TEntity> GetWithCondition(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public Task<bool> UpdateAsync(TEntity item)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            if(await SaveChangeAsync())
            {
                cache.Set(item.Id + "-" + item.GetType(), item, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CAHSE_TIME_LIFE_IN_MINUT)));
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
            return await SaveChangeAsync();
        }
        private async Task<bool> SaveChangeAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}