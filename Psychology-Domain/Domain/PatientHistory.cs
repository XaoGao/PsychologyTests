using System;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientHistory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTime DateInsert { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ReceptionResult { get; set; }
    }
}