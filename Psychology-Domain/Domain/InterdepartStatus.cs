using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Статус межведомственных запросов.
    /// </summary>
    public class InterdepartStatus : DomainEntity
    {
        /// <summary>
        /// Уровень сортровки.
        /// </summary>
        /// <value></value>
        public int LevelSort { get; set; }
        /// <summary>
        /// Наименование статуса.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}