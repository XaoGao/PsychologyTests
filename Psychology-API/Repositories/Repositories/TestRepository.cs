using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Psychology_API.Repositories.Repositories
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public TestRepository(DataContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            var test = await _context.Tests
                                .Include(t => t.Questions)
                                    .ThenInclude(q => q.Answers)
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