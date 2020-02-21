using System.Threading.Tasks;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings.InterdepartStatus;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// Класс по имитации отправки данных на сервис.
    /// </summary>
    public class SenderInerdepartRequestLocalFacad : ISenderInterdepartRequestFacad<Document>
    {
        private readonly IDocumentRepository _documentRepository;
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        /// <param name="documentRepository"></param>
        public SenderInerdepartRequestLocalFacad(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public async Task RequestAsync(Document document)
        {
            InterdepartRequest interdepartRequest;
            if(Verification(document))
            {
                interdepartRequest = new InterdepartRequest(document.Id,
                    (int)InterdepartStatusType.Confirmed);
            }
            else
            {
                interdepartRequest = new InterdepartRequest(document.Id,
                    (int)InterdepartStatusType.Denied);
            }

            _documentRepository.Add(interdepartRequest);
            await _documentRepository.SaveAllAsync();
        }
        /// <summary>
        /// Проверка данных на корректность.(имитация)
        /// </summary>
        /// <param name="document"></param>
        /// <returns> True документ валидный</returns>
        private bool Verification(Document document)
        {
            if (string.IsNullOrWhiteSpace(document.Number))
                return false;

            if (string.IsNullOrWhiteSpace(document.Series))
                return false;

            if (document.Number.Length != 6 && document.Series.Length != 4)
                return false;

            return true;
        }
    }
}