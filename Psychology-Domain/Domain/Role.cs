using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class Role : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Наименование роли.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Активность роли.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
    }
}