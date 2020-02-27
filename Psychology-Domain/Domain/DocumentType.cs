using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Типы документов.
    /// </summary>
    public class DocumentType : DomainEntity
    {
        /// <summary>
        /// Наименование типа документа.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}