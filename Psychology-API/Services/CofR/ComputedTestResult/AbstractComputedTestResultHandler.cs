using Psychology_API.ViewModels;

namespace Psychology_API.Services.CofR.ComputedTestResult
{
    /// <summary>
    /// Базовый класс для цепочки отвественных.
    /// </summary>
    public abstract class AbstractComputedTestResultHandler : IComputedTestResultHandler
    {
        public AbstractComputedTestResultHandler()
        {
            
        }
        /// <summary>
        /// Ссылка на следующий обработчик.
        /// </summary>
        private IComputedTestResultHandler next;
        /// <summary>
        /// Установить следующий обработчик в цепочке.
        /// </summary>
        /// <param name="handler"> Обработчик. </param>
        /// <returns></returns>
        public IComputedTestResultHandler SetNext(IComputedTestResultHandler handler)
        {
            next = handler;

            return handler;
        }
        /// <summary>
        /// Базовый обработчик, если не поддерживается формат документа, то записывает в лог и выдает Exception.
        /// </summary>
        /// <param name="questionsAnswers"> Входящий документа в виде массива байтов. </param>
        /// <param name="testName"> Название теста. </param>
        public virtual int ComputedTestResult(QuestionsAnswersViewModel questionsAnswers, string testName)
        {
            if (next != null)
            {
                return next.ComputedTestResult(questionsAnswers, testName);
            }
            else
            {
                int sum = 0;
                foreach (var questionsAnswer in questionsAnswers.QuestionsAnswerList)
                {
                    sum += questionsAnswer.AnswerValue;
                }
                return sum;
            }
        }
        /// <summary>
        /// Алгоритм расчета баллов.
        /// </summary>
        /// <param name="questionsAnswers"></param>
        /// <returns></returns>
        protected abstract int GetPoints(QuestionsAnswersViewModel questionsAnswers);
    }
}