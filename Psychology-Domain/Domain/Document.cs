using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Документы пациента
    /// </summary>
    public class Document : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Наименование документа.
        /// </summary>
        /// <value></value>
        public string DocName { get; set; }
        /// <summary>
        /// Серия документа.
        /// </summary>
        /// <value></value>
        public string Series { get; set; }
        /// <summary>
        /// Номер документа.
        /// </summary>
        /// <value></value>
        public string Number { get; set; }
        /// <summary>
        /// Время загрузки документа.
        /// </summary>
        /// <value></value>
        public DateTime DateUpload { get; set; }
        /// <summary>
        /// Тип документа.
        /// </summary>
        /// <value></value>
        public int DocumentTypeId { get; set; }
        public DocumentType DocumenType { get; set; }
        /// <summary>
        /// Идентификатор пациента, кому принадлежат докумнеты.
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        /// <summary>
        /// Массив байтов (сам документа в base64)
        /// </summary>
        /// <value></value>
        public byte[] Body { get; set; }
        /// <summary>
        /// Расширение документа.
        /// </summary>
        /// <value></value>
        public string Extension { get; set; }
        public void GetExtensionFromFullNameDocument()
        {
            if(!string.IsNullOrWhiteSpace(DocName))
            {
                Extension = DocName.Substring(DocName.IndexOf('.'));
            }
        }
    }
}