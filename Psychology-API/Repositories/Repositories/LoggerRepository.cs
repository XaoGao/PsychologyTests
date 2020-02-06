using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Abstarct;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly DataContext _context;

        public LoggerRepository(DataContext context)
        {
            _context = context;
        }

        public void WriteErrorLog(string message, string errorText)
        {
            var log = new Log("Error", message, errorText);
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void WriteInformerLog<T>(T entity) where T : DomainEntity
        {
            var log = new Log("Info","Remove item");
            _context.Logs.Add(log);
        }
    }
}