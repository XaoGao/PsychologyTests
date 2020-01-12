using System.Collections.Generic;

namespace Psychology_API.ViewModels
{
    /// <summary>
    /// Массив данных (Идентификатор вопроса - ответ на вопрос)
    /// </summary>
    public class QuestionsAnswers
    {
        public List<QuestionsAnswer> QuestionsAnswerList { get; set; }

        public class QuestionsAnswer
        {
            public int QuestionId { get; set; }
            public int SortLevel { get; set; }
            public int AnswerValue { get; set; }
        }
    }
}