using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : GenericController<Phone>
    {
        public PhonesController(IGenericRepository<Phone> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}