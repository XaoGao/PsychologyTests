using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Отдел.
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// Наименование отдела.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Целочисленое поле для сортировки.
        /// </summary>
        /// <value></value>
        public int SortLevel { get; set; }
    }
}