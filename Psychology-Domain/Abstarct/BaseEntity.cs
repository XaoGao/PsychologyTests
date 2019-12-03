namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Базовый класс для сущностей в которых нужен предобределный идентификатор.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
    }
}