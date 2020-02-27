using System;

namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Базовый класс для всех сущностей в БД.
    /// </summary>
    public abstract class DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Время создание записи.
        /// </summary>
        /// <value></value>
        public DateTime Create { get; set; }
        /// <summary>
        /// Время последнего обновления записи.
        /// </summary>
        /// <value></value>
        public DateTime Update { get; set; }
        /// <summary>
        /// Флаг актуальности.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }

    }
}