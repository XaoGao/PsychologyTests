using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Должность.
    /// </summary>
    public class Position : BasePhonebookEntity
    {
        /// <summary>
        /// Наименование должности.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}