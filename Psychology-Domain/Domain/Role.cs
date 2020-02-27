using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class Role : DomainEntity
    {
        /// <summary>
        /// Наименование роли.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}