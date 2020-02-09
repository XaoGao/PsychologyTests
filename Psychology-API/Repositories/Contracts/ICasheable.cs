using System;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Тригеры для работы с кешем.
    /// </summary>
    public interface ICasheable<T> where T : DomainEntity
    {
        /// <summary>
        /// Событин на добавление в кеш. 
        /// </summary>
        event Action<string, string, T> SetInCashe;
        /// <summary>
        /// Событие на взятие из кеш.
        /// </summary>
        event Func<string, string, T> GetFromCashe;
        /// <summary>
        /// Событие на удаление из кеша.
        /// </summary>
        event Action<string, string> RemoveItemInCashe;
    }
}