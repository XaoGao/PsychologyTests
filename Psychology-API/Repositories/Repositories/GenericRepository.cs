using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts.GenericRepository;

namespace Psychology_API.Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await EntityFrameworkQueryableExtensions.ToListAsync(_dbSet.AsNoTracking());

            return entities;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public IEnumerable<TEntity> GetNonLock(Func<TEntity, bool> predicate)
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
            return await SaveChangeAsync();
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