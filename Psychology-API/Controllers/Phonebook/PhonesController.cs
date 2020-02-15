using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : GenericController<Phone>
    {
        public PhonesController(IGenericService<Phone> genericService, IMapper mapper) : base(genericService, mapper)
        {
            
        }
    }
}