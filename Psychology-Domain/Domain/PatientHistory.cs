using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// История пациента.
    /// </summary>
    public class PatientHistory : DomainEntity
    {
        /// <summary>
        /// Дата записи в БД.
        /// </summary>
        /// <value></value>
        public DateTime DateInsert { get; set; }
        /// <summary>
        /// Идентификатор доктора.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Идентификатор пациента.
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        /// <summary>
        /// Результат приема.
        /// </summary>
        /// <value></value>
        public string ReceptionResult { get; set; }
    }
}