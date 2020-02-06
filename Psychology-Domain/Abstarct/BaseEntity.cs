namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Базовый класс для сущностей в которых нужен предопределный идентификатор.
    /// </summary>
    public abstract class BaseEntity : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Актуальность.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
    }
}