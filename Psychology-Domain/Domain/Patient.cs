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
    }
}