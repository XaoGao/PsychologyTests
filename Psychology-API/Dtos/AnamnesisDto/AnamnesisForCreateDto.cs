using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.AnamnesisDto
{
    public class AnamnesisForCreateDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string Conclusion { get; set; }
    }
}