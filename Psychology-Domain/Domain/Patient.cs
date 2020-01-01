using System;
using System.Collections.Generic;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Пациент.
    /// </summary>
    public class Patient
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
        /// Лечащий врач.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Актуальность пациента.
        /// </summary>
        /// <value></value>
        public bool IsDelete { get; set; }
        /// <summary>
        /// Коллекция заключении пациента.
        /// </summary>
        /// <value></value>
        public ICollection<Anamnesis> Anamneses { get; set; }
        /// <summary>
        /// Документы пациента.
        /// </summary>
        /// <value></value>
        public ICollection<Document> Documents { get; set; }
    }
}