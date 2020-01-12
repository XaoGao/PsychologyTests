using Psychology_API.ViewModels;

namespace Psychology_API.Servises.CofR.ComputedTestResult
{
    /// <summary>
    /// Цепочка ответственных для расчета баллов различных тестов.
    /// </summary>
    public interface IComputedTestResultHandler
    {
        /// <summary>
        /// Указать следующего ответственного для расчета баллов.
        /// </summary>
        /// <param name="handler"> Следующий отвественный. </param>
        /// <returns></returns>
        IComputedTestResultHandler SetNext(IComputedTestResultHandler handler);
        /// <summary>
        /// Расчет баллов.
        /// </summary>
        /// <param name="questionsAnswers"> Входящий массив вопросов и ответов. </param>
        /// <param name="testName"> Название теста. </param>
        int ComputedTestResult(QuestionsAnswers questionsAnswers, string testName);
    }
}