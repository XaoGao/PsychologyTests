using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;

namespace Psychology_API.Controllers.Phonebook
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebookService _phonebookService;
        public PhonebookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPhonebook(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var phonebook = await _phonebookService.GetPhonebookAsync();

            //TODO: добавить Dtos классы для телефоного справочника.

            return Ok(phonebook);
        }
    }
}