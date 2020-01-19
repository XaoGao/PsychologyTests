using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    public class DocController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        public DocController(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<IActionResult> UploadDocs(int doctorId, int patientId, DocForCreateDto docForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var file = docForCreateDto.FileBody;

            if (file == null || file.Length <= 0 )
                return BadRequest("Не корретный документ.");

            var document = _mapper.Map<Document>(docForCreateDto);

            if (await _documentRepository.SaveDoc(document, docForCreateDto.FileBody))
                return NoContent();

            throw new Exception("Не предвиденная ошибка в ходе добавления документа, повторите снова");
        }
    }
}