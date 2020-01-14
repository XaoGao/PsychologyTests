using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using Microsoft.Extensions.Configuration;
using System;
using Psychology_API.ViewModels;

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

        public async Task<PatientTestResult> CreateAndGetPatientTestResultAsnyc(int doctorId, int patientId, int testId ,int testResultInPoints, QuestionsAnswersViewModel questionsAnswers)
        {
            var testResult = await _context.ProcessingInterpretationOfResults.SingleOrDefaultAsync(tr => tr.TestId == testId && tr.MinValue <= testResultInPoints && tr.MaxValue >= testResultInPoints);

            if(testResult == null)
                throw new Exception("Не предвиденная ошибка, не верное расчитаны количество баллов");

            var questionsAnswersList = await CreateQuestionsAnswersAsync(patientId, testId, questionsAnswers);
            var patientTestResult = new PatientTestResult(doctorId, patientId, testId, testResultInPoints, testResult.Id, DateTime.Now, questionsAnswersList);

            await _context.PatientTestResult.AddAsync(patientTestResult);
            await _context.SaveChangesAsync();

            return patientTestResult;
        }
        /// <summary>
        /// Маппинг массива данных Вопрос-Ответ. Сохранения данных в БД.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <param name="questionsAnswers"> Входящий массив вопросов и ответов. </param>
        /// <returns> Записи БД Вопрос-Ответ. </returns>
        private async Task<ICollection<QuestionAnswer>> CreateQuestionsAnswersAsync(int patientId, int testId, QuestionsAnswersViewModel questionsAnswers)
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