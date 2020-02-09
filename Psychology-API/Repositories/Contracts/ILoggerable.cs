using System;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Тригеры для логгирования.
    /// </summary>
    public interface ILoggerable
    {
        /// <summary>
        /// Событие на оповещение.
        /// </summary>
        event Action<DomainEntity> Logger;
    }
}