using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PositionsController : GenericController<Position>
    {
        public PositionsController(IGenericRepository<Position> repo) : base(repo)
        {
        }
    }
}