using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : GenericController<Department>
    {
        public DepartmentsController(IGenericService<Department> genericService, IMapper mapper) : base(genericService, mapper)
        {
            
        }

        // [Authorize(Roles = RolesSettings.HR)]
        // [HttpPut("{id}")]   
        // public async Task<IActionResult> Update(int id, Department item)
        // {
        //     var itemFromRepo = await _repo.GetAsync(id);

        //     if(itemFromRepo == null)
        //         return BadRequest($"Данного объекта для обновленя нет");

        //     _mapper.Map(item, itemFromRepo);

        //     if(await _repo.UpdateAsync(item, typeof(Department).ToString()))
        //         return Ok(itemFromRepo);
            
        //     throw new Exception("Непредвиденая ошибка в ходе обновления данных");
        // }
    }
}