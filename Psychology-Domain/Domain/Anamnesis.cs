using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Заключение по пациенту.
    /// </summary>
    public class Anamnesis : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Время проведения осмотра.
        /// </summary>
        /// <value></value>
        public DateTime ConclusionTime { get; set; }
        /// <summary>
        /// Идентификатор пациента.
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        /// <summary>
        /// Пациент.
        /// </summary>
        /// <value></value>
        public Patient Patient { get; set; }
        /// <summary>
        /// Текст заключения.
        /// </summary>
        /// <value></value>
        public string Conclusion { get; set; }
        /// <summary>
        /// Идентификатор доктора, который написал анамез.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Указатель на последнее заключение от врача.
        /// </summary>
        /// <value></value>
        public bool IsLast { get; set; }
    }
}