using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : GenericController<Position>
    {
        public PositionsController(IGenericService<Position> genericService, IMapper mapper) : base(genericService, mapper)
        {
            
        }
    }
}