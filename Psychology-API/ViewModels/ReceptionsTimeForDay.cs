using System;
using System.Collections.Generic;

namespace Psychology_API.ViewModels
{
    /// <summary>
    /// Модель - рабочие часы доктора.
    /// </summary>
    public class ReceptionsTimeForDay
    {
        public List<DateTime> TimeReceptonsForDay {get;set;}
        /// <summary>
        /// Создать экземпляр класса.
        /// </summary>
        public ReceptionsTimeForDay(DateTime day)
        {
            GetWorkTimeForDoctor(day);
        }
        /// <summary>
        /// Получить все рабочие часы.
        /// </summary>
        private void GetWorkTimeForDoctor(DateTime day)
        {
            for (int i = 8; i < 19; i++)
            {
                DateTime timeReception = new DateTime(day.Year, day.Month, day.Day, i, 0, 0);
                TimeReceptonsForDay.Add(timeReception);
            }
        }
    }
}