using System.Collections.Generic;
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
        public async Task<IEnumerable<Test>> GetTestsAsync()
        {
            var tests = await _context.Tests.ToListAsync();

            return tests;
        }
    }
}