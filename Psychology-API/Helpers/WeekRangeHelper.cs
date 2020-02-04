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
        public static DateTime GetDateOfStartWeek(this DateTime dt, DayOfWeek time)
        {
            int diff = (7 + (dt.DayOfWeek - time)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        /// <summary>
        /// Возвращает дату конца текущей рабочей недели.
        /// </summary>
        /// <param name="time"> Дата для который определяется конец недели. </param>
        /// <returns> Дата конца недели. </returns>
        public static DateTime GetDateOfEndWeek(this DateTime dt, DayOfWeek time)
        {
            int diff = (7 + (dt.DayOfWeek - time)) % 7;
            return dt.AddDays(-1 * diff + 6).Date;
        }
    }
}