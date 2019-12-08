using System;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Заключение по пациенту.
    /// </summary>
    public class Anamnesis
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int id { get; set; }
        /// <summary>
        /// Время проведения осмотра.
        /// </summary>
        /// <value></value>
        public DateTime ConclusionTime { get; set; }
        /// <summary>
        /// Идентификатор пациента.
        /// </summary>
        /// <value></value>
        public int PatinetId { get; set; }
        /// <summary>
        /// Пациент.
        /// </summary>
        /// <value></value>
        public Patient Patient { get; set; }
        /// <summary>
        /// Текс заключения.
        /// </summary>
        /// <value></value>
        public string Conclusion { get; set; }
    }
}