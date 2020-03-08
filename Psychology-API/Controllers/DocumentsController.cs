using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.DocumentDto;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;
        public DocumentsController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }
        /// <summary>
        /// Загрузить документ в систему.
        /// </summary>
        /// <param name="doctorId"> Идентификатор регистратора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="docForCreateDto"> Данные по документу. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadDocs(int doctorId, int patientId, [FromForm]DocumentForCreateDto docForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var file = docForCreateDto.File;
            if (file == null || file.Length <= 0)
                return BadRequest("Не корретный документ.");

            var document = _mapper.Map<Document>(docForCreateDto);

            document.GetExtensionFromFullNameDocument();

            if (await _documentService.SaveDocAsync(document, docForCreateDto.File))
            {
                document.DocumentType = await _documentService.GetDocTypeAsync(document.Id);
                var documentForReturn = await _documentService.SetInterdepartId(document);
                return Ok(documentForReturn);
            }

            throw new Exception("Не предвиденная ошибка в ходе добавления документа, повторите снова");
        }
        /// <summary>
        /// Список типов документов.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocTypes()
        {
            var docTypes = await _documentService.GetDocTypesAsync();

            return Ok(docTypes);
        }
        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="doctorId"> Идентификатор регистратора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param> 
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpDelete("{documentId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDocument(int doctorId, int patientId, int documentId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var document = await _documentService.GetDocumentAsync(documentId);

            if (document == null)
                return BadRequest("Указаного документа не существует");

            _documentService.Remove(document);

            if (await _documentService.SaveAllAsync())
                return NoContent();

            // TODO: что то пошло не так, записать в БД ошибку
            throw new Exception("");
        }
        /// <summary>
        /// Получить список документов пациента.
        /// </summary>
        /// <param name="doctorId"> Идентификатро доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список документов пациента. </returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("GetDocuments")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocuments(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var documents = await _documentService.GetDocumentsAsync(patientId);

            var documentsForReturn = await _documentService.SetInterdepartId(documents);

            return Ok(documentsForReturn);
        }
        /// <summary>
        /// Данные для загрузен документа на клиенте.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("{documentId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocument(int doctorId, int patientId, int documentId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var document = await _documentService.GetDocumentAsync(documentId);

            if (document == null)
                return BadRequest("Указаного документа не существует");

            return File(document.Body, "application/" + document.Extension, document.DocName);
        }
    }
}