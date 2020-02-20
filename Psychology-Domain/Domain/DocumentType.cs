using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Типы документов.
    /// </summary>
    public class DocumentType : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Наименование типа документа.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Актуальность категории документа.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
    }
}