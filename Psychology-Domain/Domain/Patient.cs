namespace Psychology_Domain.Domain
{
    //TODO: Добавить необходимые данные для сущности
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
        public int PersonalCardNumber { get; set; }
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
        public string FullaName { get => $"{Lastname} {Firstname} {Middlename}"; }
        /// <summary>
        /// Лечащий врач.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsDelete { get; set; }
        
    }
}