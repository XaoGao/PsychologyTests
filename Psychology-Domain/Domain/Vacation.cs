using System;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Отпуск врача, время, когда он не принимает пациентов.
    /// </summary>
    public class Vacation
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор доктора.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Дата начала отпуска.
        /// </summary>
        /// <value></value>
        public DateTime StartVacation { get; set; }
        /// <summary>
        /// Дата окончания отпуска.
        /// </summary>
        /// <value></value>
        public DateTime EndVacation { get; set; }
        /// <summary>
        /// Колчество дней в отпуске.
        /// </summary>
        /// <value></value>
        public int CountDays { get; set; }
    }
}