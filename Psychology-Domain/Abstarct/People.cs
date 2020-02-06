using System;

namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Абстрактный класс человека.
    /// </summary>
    public abstract class People : DomainEntity
    {
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
    }
}