using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    public class DocController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;
        public DocController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        public async Task<IActionResult> UploadDocs(int doctorId, int patientId, [FromForm]DocForCreateDto docForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var file = docForCreateDto.File;
            if (file == null || file.Length <= 0)
                return BadRequest("Не корретный документ.");

            var document = _mapper.Map<Document>(docForCreateDto);

            document.GetExtensionFromFullNameDocument();

            if (await _documentService.SaveDocAsync(document, docForCreateDto.File))
            {
                document.DocumenType = await _documentService.GetDocTypeAsync(document.Id);
                return Ok(document);
            }

            throw new Exception("Не предвиденная ошибка в ходе добавления документа, повторите снова");
        }
        [HttpGet]
        public async Task<IActionResult> GetDocTypes()
        {
            var docTypes = await _documentService.GetDocTypesAsync();

            return Ok(docTypes);
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int doctorId, int patientId, int documentId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var document = await _documentService.GetDocumentAsync(documentId);

            if (document == null)
                return BadRequest("Указаного документа не существует");

            _documentService.Remove(document);

            if (await _documentService.SaveAllAsync())
                return NoContent();

            // TODO: что то пошло не так, записать в БД ошибку
            throw new Exception("");
        }
    }
}