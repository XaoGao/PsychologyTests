using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Servises;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _dbSet;
        private readonly ICache<TEntity> _cache;
        public GenericRepository(DataContext context, ICache<TEntity> cache)
        {
            _cache = cache;
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await EntityFrameworkQueryableExtensions.ToListAsync(_dbSet.AsNoTracking());

            return entities;
        }

        public async Task<TEntity> GetAsync(int id, string type)
        {
            TEntity entity = null;

            string key = id + "-" + type;

            if (!_cache.Get(key, out entity))
            {
                entity = await _dbSet.FindAsync(id);
                if (entity != null)
                    _cache.Set(key, entity);
            }

            return entity;
        }

        public IEnumerable<TEntity> GetWithCondition(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<bool> UpdateAsync(TEntity item)
        {
            // _context.Entry(item).State = EntityState.Modified;
            if(await SaveChangeAsync())
                return true;

            return false;
        }
        public async Task<bool> CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            if (await SaveChangeAsync())
            {
                _cache.Set(item.Id + "-" + item.GetType(), item);
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

        public async Task<TEntity> GetAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);

            return item;
        }
    }
}