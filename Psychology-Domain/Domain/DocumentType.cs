namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Типы документов.
    /// </summary>
    public class DocumentType
    {
        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Наименование типа документа.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
    }
}