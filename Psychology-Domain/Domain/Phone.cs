using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Телефон доктора.
    /// </summary>
    public class Phone : BaseEntity
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        /// <value></value>
        public string Number { get; set; }
        /// <summary>
        /// Маска телефона.
        /// </summary>
        /// <value></value>
        public string NumberMask { get => $"{Number[0]}{Number[1]}-{Number[2]}{Number[3]}-{Number[4]}{Number[5]}"; }
    }
}