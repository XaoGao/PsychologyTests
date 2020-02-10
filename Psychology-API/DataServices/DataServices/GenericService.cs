using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Abstarct;

namespace Psychology_API.DataServices.DataServices
{
    public class GenericService<TEntity> : GenericRepository<TEntity>, IGenericService<TEntity> where TEntity : BaseEntity
    {
        private readonly ICache<TEntity> _cache;
        public GenericService(DataContext context, ICache<TEntity> cache) : base(context, cache)
        {
            _cache = cache;
        }

        public async Task<bool> CreateAsync(TEntity item)
        {
            SetInCashe += _cache.Set;
            var result = await base.CreateRepositoryAsync(item);
            SetInCashe -= _cache.Set;
            return result;
        }

        public async Task<bool> DeleteAsync(TEntity item)
        {
            RemoveItemInCashe += _cache.Remove;
            var result = await base.DeleteRepositoryAsync(item);
            RemoveItemInCashe -= _cache.Remove;
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await base.GetAllRepositoryAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            GetFromCashe +=  _cache.Get;
            SetInCashe += _cache.Set;
            var item = await base.GetRepositoryAsync(id);
            GetFromCashe -=  _cache.Get;
            SetInCashe -= _cache.Set;
            return item;
        }

        public async Task<TEntity> GetAsync(int id, string type)
        {
            GetFromCashe +=  _cache.Get;
            SetInCashe += _cache.Set;
            var item = await base.GetRepositoryAsync(id);
            GetFromCashe -=  _cache.Get;
            SetInCashe -= _cache.Set;
            return item;
        }

        public IEnumerable<TEntity> GetWithCondition(Func<TEntity, bool> predicate)
        {
            return base.GetWithConditionRepository(predicate);
        }

        public async Task<bool> UpdateAsync(TEntity item)
        {
            return await base.UpdateRepositoryAsync(item);
        }

        public async Task<bool> UpdateAsync(TEntity item, string type)
        {
            RemoveItemInCashe += _cache.Remove;
            SetInCashe += _cache.Set;
            var result = await base.UpdateRepositoryAsync(item, type);
            RemoveItemInCashe -= _cache.Remove;
            SetInCashe -= _cache.Set;
            return result;
        }
    }
}