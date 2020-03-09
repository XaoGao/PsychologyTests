using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Отдел.
    /// </summary>
    public class Department : BasePhonebookEntity
    {
        /// <summary>
        /// Наименование отдела.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}