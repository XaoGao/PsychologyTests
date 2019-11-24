using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Repositories;

namespace Psychology_API.Controllers.Phonebook
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly GenericRepository<TEntity> _repo;
        public GenericController(GenericRepository<TEntity> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repo.GetAllAsync();

            return Ok(entities);
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repo.GetAsync(id);

            return Ok(entity);
        }
    }
}