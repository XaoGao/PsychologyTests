using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Abstarct;

namespace Psychology_API.DataServices.DataServices
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        private readonly ICache<TEntity> _cache;
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GenericService(DataContext context, ICache<TEntity> cache, IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
            _cache = cache;
            _genericRepository.SetInCashe += _cache.Set;
            _genericRepository.RemoveItemInCashe += _cache.Remove;
            _genericRepository.GetFromCashe += _cache.Get;
        }
        public async Task<bool> CreateAsync(TEntity item)
        {            
            return await _genericRepository.CreateRepositoryAsync(item);
        }
        public async Task<bool> DeleteAsync(TEntity item)
        {            
            return await _genericRepository.DeleteRepositoryAsync(item);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _genericRepository.GetAllRepositoryAsync();
        }
        public async Task<TEntity> GetAsync(int id)
        {
            return await _genericRepository.GetRepositoryAsync(id);
        }
        public async Task<TEntity> GetAsync(int id, string type)
        {
            return await _genericRepository.GetRepositoryAsync(id);
        }
        public IEnumerable<TEntity> GetWithCondition(Func<TEntity, bool> predicate)
        {
            return _genericRepository.GetWithConditionRepository(predicate);
        }
        public async Task<bool> UpdateAsync(TEntity item)
        {
            return await _genericRepository.UpdateRepositoryAsync(item);
        }
        public async Task<bool> UpdateAsync(TEntity item, string type)
        {
            return await _genericRepository.UpdateRepositoryAsync(item, type);
        }
    }
}