using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с приемами.
    /// </summary>
    public interface IReceptionService : IBaseService
    {
        /// <summary>
        /// Получить все приемы у конкретного врача в течении рабочей недели.
        /// </summary>
        /// <param name="doctorId"> Идентификатор врача. </param>
        /// <returns> Список приемов. </returns>
        Task<IEnumerable<Reception>> GetReseptionsAsync(int doctorId);
        /// <summary>
        /// Получить все приемы у конкретного врача в течении рабочей недели.
        /// </summary>
        /// <param name="doctorId"> Идентификатор врача. </param>
        /// <param name="now"> Текущее число. </param>
        /// <returns> Список приемов. </returns>
        Task<IEnumerable<Reception>> GetReseptionsOfCurrentWeekAsync(int doctorId, DateTime now);
        /// <summary>
        /// Проверить свободно ли время у врача для приема.
        /// </summary>
        /// <param name="doctorId"> Идентификатор врача. </param>
        /// <param name="timeReception"> Время приема. </param>
        /// <returns> True если время свободное. </returns>
        Task<bool> CheckReceptionTimeAsync(int doctorId, DateTime timeReception);
        /// <summary>
        /// Получить свободные часы приема у конкретного врача за конкретный день.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="dateTimeReception"> День приема. </param>
        /// <returns> Список свободных рабочих часов для приема. </returns>
        Task<IEnumerable<DateTime>> GetFreeReceptionTimeForDayAsync(int doctorId, DateTime dateTimeReception);
    }
}