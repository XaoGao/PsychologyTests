namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Базовый класс для сущностей телефонного справочника.
    /// </summary>
    public abstract class BasePhonebookEntity : DomainEntity
    {
        /// <summary>
        /// Целочисленое поле для сортировки.
        /// </summary>
        /// <value></value>
        public int SortLevel { get; set; }
    }
}