using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Должность.
    /// </summary>
    public class Position : BaseEntity
    {
        /// <summary>
        /// Наименование должности.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Целочисленое поле для сортировки должностей.
        /// </summary>
        /// <value></value>
        public int SortLevel { get; set; }
    }
}