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
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Anamnesis> Anamneses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ProcessingInterpretationOfResult> ProcessingInterpretationOfResults { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<QuestionAnswer> QuestionsAnswers { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<PatientTestResult> PatientTestResult { get; set; }
    }
}