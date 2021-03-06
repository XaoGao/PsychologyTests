using System.Collections.Generic;
using Psychology_API.Dtos.PatientDto;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.DoctorDto
{
    public class DoctorForListReturnDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }    
        /// <summary>
        /// Фамилия.
        /// </summary>
        /// <value></value>
        public string Lastname { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        /// <value></value>
        public string Firstname { get; set; }
        /// <summary>
        /// Отчество.
        /// </summary>
        /// <value></value>
        public string Middlename { get; set; }
        public string Fullname { get => $"{Lastname} {Firstname} {Middlename}";}
        public bool IsLock { get; set; }
        public ICollection<PatientForListDto> Patients { get; set; }
    }
}