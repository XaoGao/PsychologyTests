using Psychology_API.ViewModels;

namespace Psychology_API.Servises.CofR.ComputedTestResult
{
    /// <summary>
    /// Создать цепочку ответственных для расчета баллов в тестах.
    /// </summary>
    public class ManagerComputedTestResultHandler
    {
        private BeckDepressionInventoryTestResultHandler beckDepressionInventoryTestResultHandler;
        private SpilbergTestResultHandler spilbergTestResultHandler;
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        public ManagerComputedTestResultHandler()
        {
            beckDepressionInventoryTestResultHandler = new BeckDepressionInventoryTestResultHandler();
            spilbergTestResultHandler = new SpilbergTestResultHandler();

            beckDepressionInventoryTestResultHandler.SetNext(spilbergTestResultHandler);
        }
        /// <summary>
        /// Расчет баллов.
        /// </summary>
        /// <param name="questionsAnswers"> Входящий массив вопросов и ответов. </param>
        /// <param name="testName"> Название теста. </param>
        /// <returns></returns>
        public int GetTestResultInPoints(QuestionsAnswers questionsAnswers, string testName)
        {
            return beckDepressionInventoryTestResultHandler.ComputedTestResult(questionsAnswers, testName);
        }
    }
}