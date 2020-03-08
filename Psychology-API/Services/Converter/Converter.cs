using Newtonsoft.Json;
using System.Text;

namespace Psychology_API.Services.Converter
{
    /// <summary>
    /// Конвертер по сериализации и десериализации объектов.
    /// </summary>
    /// <typeparam name="T"> Класс </typeparam>
    public class Converter<T> where T : class
    {
        /// <summary>
        /// Конвертация класса в массив байтов.
        /// </summary>
        /// <param name="entity"> Класс, который нужно конвертировать. </param>
        /// <returns> Массив байтов. </returns>
        public byte[] Serialze(T entity)
        {
            string body = JsonConvert.SerializeObject(entity);
            var result  = Encoding.UTF8.GetBytes(body);

            return result;
        }
        /// <summary>
        /// Конвертация массива байтов в класс.
        /// </summary>
        /// <param name="entity"> Массив байтов. </param>
        /// <returns> Экземпляр класса. </returns>
        public T Desiarile(byte[] entity)
        {
            var body = Encoding.UTF8.GetString(entity);
            var result = JsonConvert.DeserializeObject<T>(body);

            return result;
        }
    }
}