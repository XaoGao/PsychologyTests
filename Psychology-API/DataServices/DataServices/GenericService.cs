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
        }

        public async Task<bool> CreateAsync(TEntity item)
        {
            _genericRepository.SetInCashe += _cache.Set;
            var result = await _genericRepository.CreateRepositoryAsync(item);
            _genericRepository.SetInCashe -= _cache.Set;
            return result;
        }

        public async Task<bool> DeleteAsync(TEntity item)
        {
            _genericRepository.RemoveItemInCashe += _cache.Remove;
            var result = await _genericRepository.DeleteRepositoryAsync(item);
            _genericRepository.RemoveItemInCashe -= _cache.Remove;
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _genericRepository.GetAllRepositoryAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            AddGetSetEvents();
            var item = await _genericRepository.GetRepositoryAsync(id);
            RemoveGetSetEvents();
            return item;
        }

        public async Task<TEntity> GetAsync(int id, string type)
        {
            AddGetSetEvents();
            var item = await _genericRepository.GetRepositoryAsync(id);
            RemoveGetSetEvents();
            return item;
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
            _genericRepository.RemoveItemInCashe += _cache.Remove;
            _genericRepository.SetInCashe += _cache.Set;
            var result = await _genericRepository.UpdateRepositoryAsync(item, type);
            _genericRepository.RemoveItemInCashe -= _cache.Remove;
            _genericRepository.SetInCashe -= _cache.Set;
            return result;
        }
        /// <summary>
        /// Добавление событии на получения и внесения в кеш.
        /// </summary>
        private void AddGetSetEvents()
        {
            _genericRepository.GetFromCashe += _cache.Get;
            _genericRepository.SetInCashe += _cache.Set;
        }
        /// <summary>
        /// Убрать событии по получению и внесению в кеш.
        /// </summary>
        private void RemoveGetSetEvents()
        {
            _genericRepository.GetFromCashe -= _cache.Get;
            _genericRepository.SetInCashe -= _cache.Set;
        }
    }
}