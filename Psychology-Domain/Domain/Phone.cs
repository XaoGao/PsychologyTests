namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Телефон доктора.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Номер телефона.
        /// </summary>
        /// <value></value>
        public string Number { get; set; }
        /// <summary>
        /// Актуальность телефона.
        /// </summary>
        /// <value></value>
        public bool IsLock { get; set; }
        /// <summary>
        /// Маска телефона.
        /// </summary>
        /// <value></value>
        public string NumberMask { get => $"{Number[0]}{Number[1]}-{Number[2]}{Number[3]}-{Number[4]}{Number[5]}"; }
    }
}