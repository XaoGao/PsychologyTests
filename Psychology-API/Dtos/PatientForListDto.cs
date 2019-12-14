using System;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos
{
    public class PatientForListDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Номер личной карточки.
        /// </summary>
        /// <value></value>
        public string PersonalCardNumber { get; set; }
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
        /// <summary>
        /// ФИО.
        /// </summary>
        /// <value></value>
        public string Fullname { get => $"{Lastname} {Firstname} {Middlename}"; }
        /// <summary>
        /// Дата рождения.
        /// </summary>
        /// <value></value>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        ///  Заключение пациента.
        /// </summary>
        /// <value></value>
        public string Conclusion { get; set; }
    }
}