using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : GenericController<Department>
    {
        private readonly IGenericRepository<Department> _repo;
        private readonly IMapper _mapper;

        public DepartmentsController(IGenericRepository<Department> repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [Authorize(Roles = RolesSettings.HR)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department item)
        {
            var itemFromRepo = await _repo.GetAsync(id, typeof(Department).ToString());

            if(itemFromRepo == null)
                return BadRequest($"Данного объекта для обновленя нет");

            _mapper.Map(item, itemFromRepo);

            if(await _repo.UpdateAsync(item))
                return Ok(itemFromRepo);
            
            throw new Exception("Непредвиденая ошибка в ходе обновления данных");
        }
    }
}