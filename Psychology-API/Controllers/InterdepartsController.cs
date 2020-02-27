using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class InterdepartsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public InterdepartsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPut("document/{documentId}/request")]
        public async Task<IActionResult> RequestInterdepart(int documentId)
        {
            var document = await _documentService.GetDocumentAsync(documentId);

            if (document == null)
                return BadRequest("Указаного документа не существует");

            await _documentService.RequestInterdepart(document);

            return NoContent();
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPut("changeinterdepart")]
        public IActionResult ChangeInterdepart(string interdepartType)
        {
            _documentService.ChangeInterdepartDeprtment(interdepartType);

            return NoContent();
        }
    }
}