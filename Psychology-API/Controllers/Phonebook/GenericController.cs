using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Psychology_Domain.Abstarct;
using AutoMapper;
using Psychology_API.Settings;
using Psychology_API.DataServices.Contracts;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<TEntity> _genericService;

        public GenericController(IGenericService<TEntity> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(bool param = true)
        {
            //param - указатель, получить все значения или только актуальные
            if (param)
                return Ok(await _genericService.GetAllAsync());
            else
                return Ok(_genericService.GetWithCondition(e => e.IsLock != true));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _genericService.GetAsync(id, typeof(TEntity).ToString());

            if (entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            return Ok(entity);
        }
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPost("{doctorId}")]
        public async Task<IActionResult> Create(int doctorId, TEntity item)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            if (await _genericService.CreateAsync(item))
                return Ok(item);//Ok("Данные успешно добавлены.");

            throw new Exception("Не предвиденная ошибка в ходе добавления новых данных.");
        }
        #region  GenericUpdate
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TEntity item)
        {
            var itemFromRepo = await _genericService.GetAsync(id, typeof(TEntity).ToString());

            if(itemFromRepo == null)
                return BadRequest($"Данного объекта для обновленя нет");

            _mapper.Map(item, itemFromRepo);

            if(await _genericService.UpdateAsync(item))
                return Ok(itemFromRepo);

            throw new Exception("Непредвиденая ошибка в ходе обновления данных");
        }
        #endregion

        [Authorize(Roles = RolesSettings.HR)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            var entity = await _genericService.GetAsync(id, typeof(TEntity).ToString());

            if (entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            if (await _genericService.DeleteAsync(entity))
                return Ok("Данные успешно удалены.");

            throw new Exception("Запрашиваемых данных не существует.");
        }
    }
}