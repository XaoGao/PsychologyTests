using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Статус межведомственных запросов.
    /// </summary>
    public class InterdepartStatus : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
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
        /// <summary>
        /// Актуальность статуса.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
    }
}