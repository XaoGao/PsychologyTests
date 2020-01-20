using System;

namespace Psychology_API.Dtos
{
    public class ReceptionForReturnDto
    {
        public int Id { get; set; }
        public DateTime DateTimeReception { get; set; }
        public string Fullname { get; set; }
    }
}