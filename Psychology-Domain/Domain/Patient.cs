using System.Collections.Generic;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Пациент.
    /// </summary>
    public class Patient : People
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Номер личной карточки.
        /// </summary>
        /// <value></value>
        public string PersonalCardNumber { get; set; }
        /// <summary>
        /// Лечащий врач.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Актуальность пациента.
        /// </summary>
        /// <value></value>
        public bool IsDelete { get; set; }
        /// <summary>
        /// Коллекция заключении пациента.
        /// </summary>
        /// <value></value>
        public ICollection<Anamnesis> Anamneses { get; set; }
        /// <summary>
        /// Документы пациента.
        /// </summary>
        /// <value></value>
        public ICollection<Document> Documents { get; set; }
    }
}