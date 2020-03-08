using System;
using System.Threading.Tasks;
using AutoMapper;
using Psychology_API.Dtos.InterdepartDto;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Services.Converter;
using Psychology_API.Services.RabbitMQ;
using Psychology_API.Settings.InterdepartStatus;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// Класс отправки данных на реальный сервис.
    /// </summary>
    public class SenderInerdepartRequestFacad : ISenderInterdepartRequestFacad<Document>
    {
        private readonly IMapper _mapper;
        private readonly IBroker _broker;
        private readonly IDocumentRepository _documentRepository;
        private readonly Converter<InterdepartRequestDto> _converter;
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="documentRepository"></param>
        /// <param name="serviceProvider"></param>
        public SenderInerdepartRequestFacad(IMapper mapper, IDocumentRepository documentRepository, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _broker = new Rabbit(serviceProvider);
            _documentRepository = documentRepository;
            _converter = new Converter<InterdepartRequestDto>();
        }
        public async Task RequestAsync(Document entity)
        {
            var interdepartRequest = await _documentRepository.GetInterdepartRequestRepositoryAsync(entity.Id);
            interdepartRequest.Document = entity;

            var interdepartRequestDto = _mapper.Map<InterdepartRequestDto>(interdepartRequest);

            var bodyRequest = _converter.Serialze(interdepartRequestDto);

            if (_broker.Request(bodyRequest))
            {
                interdepartRequest.InterdepartStatusId = (int)InterdepartStatusType.RequestHasBeenSent;
                await _documentRepository.SaveAllAsync();
            }
        }
    }
}