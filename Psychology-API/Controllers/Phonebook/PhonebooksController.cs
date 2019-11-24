using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Controllers.Phonebook
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PhonebooksController : ControllerBase
    {
        private readonly IPhonebookRepository _phonebookRepository;
        public PhonebooksController(IPhonebookRepository phonebookRepository)
        {
            _phonebookRepository = phonebookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPhonebook(int doctorId)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var phonebook = await _phonebookRepository.GetPhonebookAsync();

            //TODO: добавить Dtos классы для телефоного справочника.

            return Ok(phonebook);
        }
    }
}