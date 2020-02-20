using System.Collections.Generic;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Врач.
    /// </summary>
    public class Doctor : People
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
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
        /// Пациенты под надзором данного врача.
        /// </summary>
        /// <value></value>
        public ICollection<Patient> Patients { get; set; }
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
        /// Идентификатор телефона, который числится за доктором.
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
        /// <summary>
        /// Роль пользователя.
        /// </summary>
        /// <value></value>
        public Role Role { get; set; }
        /// <summary>
        /// Флаг для удаленных врачей.
        /// </summary>
        /// <value></value>
        public bool IsDeleted { get; set; }
    }
}