using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Psychology_API.Services.Converter
{
    public class ConverterToByte<T> where T : class
    {
        public byte[] Serialze(T entity)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {

                formatter.Serialize(ms, entity);

                ms.Position = 0;

                StreamReader st = new StreamReader(ms);

                var result = st.ReadToEnd();

                return System.Text.Encoding.UTF8.GetBytes(result);
            }      
        }
        public T Desiarile(byte[] entity)
        {
            var formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {   
                if (ms.Length > 0 && formatter.Deserialize(ms) is T items)
                    return items;
                else
                    return default(T);
            }      
        }
    }
}