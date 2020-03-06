using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.PhonebookDto;

namespace Psychology_API.Controllers.Phonebook
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebookService _phonebookService;
        private readonly IMapper _mapper;

        public PhonebookController(IPhonebookService phonebookService, IMapper mapper)
        {
            _phonebookService = phonebookService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPhonebook(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var phonebook = await _phonebookService.GetPhonebookAsync();

            var phonebookForReturn = _mapper.Map<IEnumerable<DepartmentWithDoctorsDto>>(phonebook);

            return Ok(phonebookForReturn);
        }
    }
}