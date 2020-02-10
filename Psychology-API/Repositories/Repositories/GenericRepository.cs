using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _dbSet;
        private readonly ICache<TEntity> _cache;

        public event Action<string, string, TEntity> SetInCashe;
        public event Func<string, string, TEntity> GetFromCashe;
        public event Action<string, string> RemoveItemInCashe;

        public GenericRepository(DataContext context, ICache<TEntity> cache)
        {
            _cache = cache;
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllRepositoryAsync()
        {
            var entities = await EntityFrameworkQueryableExtensions.ToListAsync(_dbSet.AsNoTracking());

            return entities;
        }

        public async Task<TEntity> GetRepositoryAsync(int id, string type)
        {
            TEntity entity = GetFromCashe(id.ToString(),type);

            if (entity == null)
            {
                entity = await _dbSet.FindAsync(id);
                if (entity != null)
                    SetInCashe(id.ToString(), type, entity);
            }

            return entity;
        }

        public IEnumerable<TEntity> GetWithConditionRepository(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<bool> UpdateRepositoryAsync(TEntity item)
        {
            if(await SaveChangeAsync())
                return true;

            return false;
        }
        public async Task<bool> CreateRepositoryAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            if (await SaveChangeAsync())
            {
                SetInCashe(item.Id.ToString(), item.GetType().ToString(), item);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRepositoryAsync(TEntity item)
        {
            RemoveItemInCashe(item.Id.ToString(), item.GetType().ToString());
            _dbSet.Remove(item);
            return await SaveChangeAsync();
        }
        private async Task<bool> SaveChangeAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<TEntity> GetRepositoryAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);

            return item;
        }

        public async Task<bool> UpdateRepositoryAsync(TEntity item, string type)
        {
            RemoveItemInCashe(item.Id.ToString(), type);
            if(await SaveChangeAsync())
            {
                SetInCashe(item.Id.ToString(), item.GetType().ToString(), item);
                return true;
            }

            return false;
        }
    }
}