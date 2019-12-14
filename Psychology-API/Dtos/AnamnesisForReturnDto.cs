using System;

namespace Psychology_API.Dtos
{
    public class AnamnesisForReturnDto
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
        public PatientForListDto Patient { get; set; }
        /// <summary>
        /// Текст заключения.
        /// </summary>
        /// <value></value>
        public string Conclusion { get; set; }
        public DoctorForListReturnDto Doctor { get; set; }
        /// <summary>
        /// Указатель на последнее заключение от врача.
        /// </summary>
        /// <value></value>
        public bool IsLast { get; set; }
    }
}