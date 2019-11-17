using System.Collections.Generic;
using System;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Врач.
    /// </summary>
    public class Doctor
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
        /// Хэшированый пароль.
        /// </summary>
        /// <value></value>
        public byte[] PasswordHash { get; set; }
        /// <summary>
        /// Соль для хэш пароля.
        /// </summary>
        /// <value></value>
        public byte[] PasswordSalt { get; set; }
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
        /// Пациенты под надзором данного врача.
        /// </summary>
        /// <value></value>
        public ICollection<Patient> Patients { get; set; }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="username"> Логин. </param>
        /// <param name="passwordHash"> Хэш пароль. </param>
        /// <param name="passwordSalt">Соля для хэш пароля. </param>
        /// <param name="lastname"> Фамилия. </param>
        /// <param name="firstname"> Имя. </param>
        /// <param name="middlename">Отчество. </param>
        // public Doctor(string username, byte[] passwordHash, byte[] passwordSalt, string lastname, string firstname, string middlename)
        // {
        //     #region Проверка входных параметров
        //     if(string.IsNullOrWhiteSpace(username))
        //         throw new ArgumentNullException(nameof(username));
        //     if(string.IsNullOrWhiteSpace(lastname))
        //         throw new ArgumentNullException(nameof(lastname));
        //     if(string.IsNullOrWhiteSpace(firstname))
        //         throw new ArgumentNullException(nameof(firstname));
        //     if(string.IsNullOrWhiteSpace(middlename))
        //         throw new ArgumentNullException(nameof(middlename));
        //     if((passwordSalt.Length <= 0) || (passwordHash.Length <= 0))
        //         throw new ArgumentNullException(nameof(PasswordHash));
        //     #endregion
            
        //     #region Инициализация полей класса
        //     Username = username;
        //     PasswordHash = passwordHash;
        //     PasswordSalt = passwordSalt;
        //     Lastname = lastname;
        //     Firstname = firstname;
        //     Middlename = middlename;
        //     #endregion
        // }
    }
}