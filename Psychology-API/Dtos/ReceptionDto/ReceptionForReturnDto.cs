using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos
{
    public class ReceptionForReturnDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DateTimeReception { get; set; }
        [Required]
        public string Fullname { get; set; }
    }
}