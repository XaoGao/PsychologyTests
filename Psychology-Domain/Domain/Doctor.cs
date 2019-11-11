using System.Collections.Generic;

namespace Psychology_Domain.Domain
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}