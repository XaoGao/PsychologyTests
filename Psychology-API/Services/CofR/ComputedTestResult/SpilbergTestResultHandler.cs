using System.Linq;
using Psychology_API.ViewModels;

namespace Psychology_API.Services.CofR.ComputedTestResult
{
    /// <summary>
    /// Класс в цепоче ответственных для расчета баллов для теста Спилбергера.
    /// </summary>
    public class SpilbergTestResultHandler : AbstractComputedTestResultHandler
    {
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public SpilbergTestResultHandler()
        {
            
        }
        /// <summary>
        /// Расчет баллов для результирующих баллов для теста Тест Спилбергера
        /// </summary>
        /// <param name="questionsAnswers"> Перечень вопросов и ответов на них. </param>
        /// <param name="testName"> Наименование теста. </param>
        /// <returns></returns>
        public override int ComputedTestResult(QuestionsAnswersViewModel questionsAnswers, string testName)
        {
            if(testName.Equals("Тест Спилбергера"))
            {
                return GetPoints(questionsAnswers);
            }
            else
            {
                return base.ComputedTestResult(questionsAnswers, testName);
            }
        }
        
        protected override int GetPoints(QuestionsAnswersViewModel questionsAnswers)
        {
            int[] directQuestions = new int[] {3,4,6,7,9,12,13,14,17,18,22,23,24,25,28,29,31,32,34,35,37,38,40};
            int[] backQuestions = new int[] {1,2,5,8,10,11,15,16,19,20,21,26,27,30,33,36,39};
            int sumDirectQuestions = 0;
            int sumBackQuestions = 0;
            foreach (var item in questionsAnswers.QuestionsAnswerList)
            {
                if(directQuestions.Any(x => x == item.SortLevel))
                    sumDirectQuestions += item.AnswerValue;
                    
                if(backQuestions.Any(x => x == item.SortLevel))
                    sumBackQuestions += item.AnswerValue;
            }
            var result = sumDirectQuestions - sumBackQuestions + 40;

            return result;
        }
    }
}