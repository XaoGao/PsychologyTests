using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        private readonly DataContext _context;
        public TestRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            var test = await _context.Tests
                                .Include(t => t.Answers)
                                .Include(t => t.Questions)
                                .SingleOrDefaultAsync(t => t.Id == testId);

            return test;
        }

        public async Task<IEnumerable<Test>> GetTestsAsync()
        {
            var tests = await _context.Tests.ToListAsync();

            return tests;
        }
    }
}