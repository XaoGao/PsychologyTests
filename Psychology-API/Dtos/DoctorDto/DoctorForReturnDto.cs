using System;
using System.Collections.Generic;
using Psychology_API.Dtos.PatientDto;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.DoctorDto
{
    public class DoctorForReturnDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Логин.
        /// </summary>
        /// <value></value>
        public string Username { get; set; }
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
        /// <summary>
        /// Пациенты под надзором данного врача.
        /// </summary>
        /// <value></value>
        public ICollection<PatientForListDto> Patients { get; set; }
        /// <summary>
        /// Идентификатор отдела в котором работает доктор.
        /// </summary>
        /// <value></value>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Отдел.
        /// </summary>
        /// <value></value>
        public Department Department { get; set; }
        /// <summary>
        /// Идентификатор должности которая назначина доктору.
        /// </summary>
        /// <value></value>
        public int PositionId { get; set; }
        /// <summary>
        /// Должность.
        /// </summary>
        /// <value></value>
        public Position Position { get; set; }
        /// <summary>
        /// Идентификатора телефона, который числится за доктором.
        /// </summary>
        /// <value></value>
        public int PhoneId { get; set; }
        /// <summary>
        /// Телефон.
        /// </summary>
        /// <value></value>
        public Phone Phone { get; set; }
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        /// <value></value>
        public int RoleId { get; set; }
        public bool IsLock { get; set; }
    }
}