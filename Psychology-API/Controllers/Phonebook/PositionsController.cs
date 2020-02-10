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
    public class PositionsController : GenericController<Position>
    {
        public PositionsController(IGenericService<Position> genericService, IMapper mapper) : base(genericService, mapper)
        {
            
        }

        
        // [Authorize(Roles = RolesSettings.HR)]
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, Position item)
        // {
        //     var itemFromRepo = await _repo.GetAsync(id);

        //     if(itemFromRepo == null)
        //         return BadRequest($"Данного объекта для обновленя нет");

        //     _mapper.Map(item, itemFromRepo);

        //     if(await _repo.UpdateAsync(item, typeof(Position).ToString()))
        //         return Ok(itemFromRepo);
            
        //     throw new Exception("Непредвиденая ошибка в ходе обновления данных");
        // }
    }
}