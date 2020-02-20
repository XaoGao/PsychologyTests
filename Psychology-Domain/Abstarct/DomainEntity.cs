using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Psychology_Domain.Abstarct
{
    /// <summary>
    /// Базовый класс для всех сущностей в БД.
    /// </summary>
    public abstract class DomainEntity
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}