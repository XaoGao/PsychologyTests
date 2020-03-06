using System;
using System.Collections.Generic;
using Psychology_API.Dtos.AnamnesisDto;
using Psychology_API.Dtos.DoctorDto;
using Psychology_API.Dtos.DocumentDto;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.PatientDto
{
    public class PatientForReturnDto
    {
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
        /// <summary>
        /// Дата рождения.
        /// </summary>
        /// <value></value>
        public DateTime DateOfBirth { get; set; }
        public string PersonalCardNumber { get; set; }
        /// <summary>
        /// Лечащий врач.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public DoctorForReturnDto Doctor { get; set; }
        /// <summary>
        /// Коллекция заключении пациента.
        /// </summary>
        /// <value></value>
        public ICollection<AnamnesisForReturnDto> Anamneses { get; set; }
        /// <summary>
        /// Документы пациента.
        /// </summary>
        /// <value></value>
        public ICollection<DocumentForReturnListDto> Documents { get; set; }
    }
}