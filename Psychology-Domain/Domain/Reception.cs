using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Прием у врача.
    /// </summary>
    public class Reception : DomainEntity
    {
        /// <summary>
        /// Время приема пациента.
        /// </summary>
        /// <value></value>
        public DateTime DateTimeReception { get; set; }
        /// <summary>
        /// Идентификатор врача.
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
    }
}