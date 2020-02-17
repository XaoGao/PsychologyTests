using Psychology_API.Dtos;
using Psychology_API.Services.Converter;
using Psychology_API.Services.RabbitMQ;

namespace Psychology_API.Services.Interdepart
{
    public class InterdepartFacade
    {
        private readonly IBroker _broker;
        private readonly Converter<DocumentForInterdepartRequestDto> _converter;
        public InterdepartFacade(IBroker broker)
        {
            _broker = broker;
            _converter = new Converter<DocumentForInterdepartRequestDto>();
        }
        public void Request(DocumentForInterdepartRequestDto document)
        {
            var bodyForMessage = _converter.Serialze(document);
            _broker.Request(bodyForMessage);
        }
        public void Response()
        {
            var result =_broker.Response();
        }

    }
}