using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// Класс по работе с межведомственными запросами.
    /// </summary>
    public class SenderInterdepartRequest : ISenderInterdepartRequest
    {
        /// <summary>
        /// Логика работы межведомственного запроса.
        /// </summary>
        private ISenderInterdepartRequestFacad<Document> _senderInterdepartRequestFacad;
        private Dictionary<string, ISenderInterdepartRequestFacad<Document>> container;
        public const string LOCAL = "local";
        public const string REAL = "real";
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="documentRepository"></param>
        /// <param name="serviceProvider"></param>
        public SenderInterdepartRequest(IMapper mapper, IDocumentRepository documentRepository, IServiceProvider serviceProvider)
        {
            InitDictionary(mapper, documentRepository, serviceProvider);
            _senderInterdepartRequestFacad = container[REAL];
        }
        public async Task<bool> RequestAsync(Document document)
        {
            try
            {
                await _senderInterdepartRequestFacad.RequestAsync(document);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Получить из словаря класс отвечающий за отправку межведомственных запросов.
        /// </summary>
        /// <param name="senderInterdepartRequestFacadKey"> Ключ для словаря. </param>
        public void ChangeInterdepartDeprtment(string senderInterdepartRequestFacadKey)
        {
            _senderInterdepartRequestFacad = container[senderInterdepartRequestFacadKey];
        }
        /// <summary>
        /// Инициализировать словарь.
        /// </summary>
        /// <param name="mapper"> </param>
        /// <param name="documentRepository"></param>
        /// <param name="serviceProvider"></param>
        private void InitDictionary(IMapper mapper, IDocumentRepository documentRepository, IServiceProvider serviceProvider)
        {
            container = new Dictionary<string, ISenderInterdepartRequestFacad<Document>>();
            container.Add(REAL, new SenderInerdepartRequestFacad(mapper, documentRepository, serviceProvider));
            container.Add(LOCAL, new SenderInerdepartRequestLocalFacad(documentRepository));
        }
    }
}