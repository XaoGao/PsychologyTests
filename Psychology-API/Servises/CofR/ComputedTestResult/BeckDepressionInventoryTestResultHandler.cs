using Microsoft.Extensions.Logging;
using Psychology_API.ViewModels;

namespace Psychology_API.Servises.CofR.ComputedTestResult
{
    /// <summary>
    /// 
    /// </summary>
    public class BeckDepressionInventoryTestResultHandler : AbstractComputedTestResultHandler
    {
        private readonly ILogger<AbstractComputedTestResultHandler> _logger;
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public BeckDepressionInventoryTestResultHandler()
        {
        }
        /// <summary>
        /// Расчет баллов для результирующих баллов для теста Шкала депрессии Бека
        /// </summary>
        /// <param name="questionsAnswers"> Перечень вопросов и ответов на них. </param>
        /// <param name="testName"> Наименование теста. </param>
        /// <returns></returns>
        public override int ComputedTestResult(QuestionsAnswers questionsAnswers, string testName)
        {
            if(testName.Equals("Шкала депрессии Бека"))
            {
                return base.ComputedTestResult(questionsAnswers, testName);
            }
            else
            {
                return base.ComputedTestResult(questionsAnswers, testName);
            }
        }
    }
}