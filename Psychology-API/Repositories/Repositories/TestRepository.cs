using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using Microsoft.Extensions.Configuration;
using System;

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

        public async Task<PatientTestResult> CreateAndGetPatientTestResultAsnyc(int doctorId, int patientId, int testResultInPoints)
        {
            var testResult = await _context.ProcessingInterpretationOfResults
                .SingleOrDefaultAsync(tr => testResultInPoints > tr.MinValue && testResultInPoints < tr.MaxValue);

            if(testResult == null)
                throw new Exception("Не предвиденная ошибка, не верное расчитаны количество баллов");

            var patientTestResult = new PatientTestResult();

            patientTestResult.DoctorId = doctorId;
            patientTestResult.PatientId = patientId;
            patientTestResult.ProcessingInterpretationOfResultId = testResult.Id;
            patientTestResult.DateTimeCreate = DateTime.Now;

            await _context.PatientTestResult.AddAsync(patientTestResult);
            await _context.SaveChangesAsync();

            return patientTestResult;
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