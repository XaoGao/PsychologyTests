using System;
using Psychology_API.Dtos.DoctorDto;

namespace Psychology_API.Dtos.VacationDto
{
    public class VacationForReturnListDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DoctorForListReturnDto Doctor { get; set; }
        public DateTime StartVacation { get; set; }
        public DateTime EndVacation { get; set; }
        public int CountDays { get; set; }
    }
}