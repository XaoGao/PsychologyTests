using System.Collections.Generic;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.PhonebookDto
{
    public class DepartmentWithDoctorsDto
    {
        public Department Department { get; set; }
        public List<DoctorForReturnPhonebookDto> Doctors { get; set; }
    }
}