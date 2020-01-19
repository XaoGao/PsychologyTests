using System;

namespace Psychology_API.Helpers
{
    /// <summary>
    /// Класс для определения начала и конца текщуй недели.
    /// </summary>
    public static class WeekRangeHelper
    {
        /// <summary>
        /// Возвращает дату начала текущей рабочей недели.
        /// </summary>
        /// <param name="time"> Дата для который определяется начало недели. </param>
        /// <returns> Дата начала недели. </returns>
        public static DateTime GetStartWeek(DateTime time)
        {
            return DateTime.Now;
        }
        /// <summary>
        /// Возвращает дату конца текущей рабочей недели.
        /// </summary>
        /// <param name="time"> Дата для который определяется конец недели. </param>
        /// <returns> Дата конца недели. </returns>
        public static DateTime GetEndWeek(DateTime time)
        {
            return DateTime.Now;
        }
    }
    enum WeekDay
    {
        Sunday = 0,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturady
    }
}