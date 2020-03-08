using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = RolesSettings.Registry)]
    [ApiController]
    [Route("api/[controller]")]
    public class InterdepartsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public InterdepartsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        /// <summary>
        /// Осуществить межведомственный закпрос.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns></returns>
        [HttpPut("document/{documentId}/request")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RequestInterdepart(int documentId)
        {
            var document = await _documentService.GetDocumentAsync(documentId);

            if (document == null)
                return BadRequest("Указаного документа не существует");

            await _documentService.RequestInterdepart(document);

            return NoContent();
        }
        /// <summary>
        /// Сменить логику осуществления межведомственного запроса.
        /// </summary>
        /// <param name="interdepartType"> Ключ межведомственного запроса. (local, real)</param>
        /// <returns></returns>
        [HttpPut("changeinterdepart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ChangeInterdepart(string interdepartType)
        {
            _documentService.ChangeInterdepartDeprtment(interdepartType);

            return NoContent();
        }
    }
}