using Microsoft.EntityFrameworkCore;
using Psychology_Domain.Domain;

namespace Psychology_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientHistory> PatientHistories { get; set; }
        // public DbSet<Test> Tests { get; set; }
    }
}