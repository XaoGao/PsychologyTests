namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Должность.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
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
        /// <summary>
        /// Актуальность должности.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
    }
}