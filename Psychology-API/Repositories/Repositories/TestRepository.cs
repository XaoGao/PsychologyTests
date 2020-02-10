using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using Microsoft.Extensions.Configuration;
using System;
using Psychology_API.ViewModels;
using System.Linq;

namespace Psychology_API.Repositories.Repositories
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        private readonly DataContext _context;
        public TestRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PatientTestResult> CreateAndGetPatientTestResultRepositoryAsnyc(int doctorId, int patientId, int testId ,int testResultInPoints, QuestionsAnswersViewModel questionsAnswers)
        {
            var testResult = await _context.ProcessingInterpretationOfResults.SingleOrDefaultAsync(tr => tr.TestId == testId && 
                tr.MinValue <= testResultInPoints && 
                tr.MaxValue >= testResultInPoints);

            if(testResult == null)
                throw new Exception("Не предвиденная ошибка, не верное расчитаны количество баллов");

            var questionsAnswersList = await CreateQuestionsAnswersRepositoryAsync(patientId, testId, questionsAnswers);
            var patientTestResult = new PatientTestResult(doctorId, patientId, testId, testResultInPoints, testResult.Id, DateTime.Now, questionsAnswersList);

            await _context.PatientTestResult.AddAsync(patientTestResult);
            await _context.SaveChangesAsync();

            return patientTestResult;
        }
        public async Task<Test> GetTestRepositoryAsync(int testId)
        {
            var test = await _context.Tests
                                .Include(t => t.Questions)
                                    .ThenInclude(q => q.Answers)
                                .SingleOrDefaultAsync(t => t.Id == testId);

            return test;
        }
        public async Task<IEnumerable<Test>> GetTestsRepositoryAsync()
        {
            var tests = await _context.Tests.ToListAsync();

            return tests;
        }
        public async Task<IEnumerable<PatientTestResult>> GetTestsHistiryOfPatientRepositoryAsync(int patientId)
        {
            var patientTestResults = await _context.PatientTestResult
                .Include(ptr => ptr.Doctor)
                .Include(ptr => ptr.Patient)
                .Include(ptr => ptr.Test)
                .Include(ptr => ptr.ProcessingInterpretationOfResult)
                .OrderByDescending(ptr => ptr.DateTimeCreate)
                .ToListAsync();

            return patientTestResults;
        }
        public async Task<PatientTestResult> GetTestHistiryOfPatientRepositoryAsync(int patientTestResultId)
        {
            var patientTestResult = await _context.PatientTestResult
                .Include(ptr => ptr.Test)
                .Include(ptr => ptr.ProcessingInterpretationOfResult)
                .Include(ptr => ptr.QuestionsAnswers)
                    .ThenInclude(q => q.Question)
                    .ThenInclude(a => a.Answers)
                .SingleOrDefaultAsync(ptr => ptr.Id == patientTestResultId);

            return patientTestResult;
        }
        /// <summary>
        /// Маппинг массива данных Вопрос-Ответ. Сохранения данных в БД.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <param name="questionsAnswers"> Входящий массив вопросов и ответов. </param>
        /// <returns> Записи БД Вопрос-Ответ. </returns>
        private async Task<ICollection<QuestionAnswer>> CreateQuestionsAnswersRepositoryAsync(int patientId, int testId, QuestionsAnswersViewModel questionsAnswers)
        {
            List<QuestionAnswer> questionAnswersList = new List<QuestionAnswer>();
            foreach (var item in questionsAnswers.QuestionsAnswerList)
            {
                QuestionAnswer questionAnswer = new QuestionAnswer()
                {
                    PatientId = patientId,
                    TestId = testId,
                    QuestionId = item.QuestionId,
                    AnswersValue = item.AnswerValue
                };
                await _context.QuestionsAnswers.AddAsync(questionAnswer);
                await _context.SaveChangesAsync();
                questionAnswersList.Add(questionAnswer);
            }
            return questionAnswersList;
        }
    }
}