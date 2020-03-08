using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Psychology_Domain.Abstarct;
using AutoMapper;
using Psychology_API.Settings;
using Psychology_API.DataServices.Contracts;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Controllers.Phonebook
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : BasePhonebookEntity
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<TEntity> _genericService;

        public GenericController(IGenericService<TEntity> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }
        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <param name="param"> Параметр актуальности. </param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]    
        public async Task<IActionResult> GetAll(bool param = true)
        {
            //param - указатель, получить все значения или только актуальные
            if (param)
                return Ok(await _genericService.GetAllAsync());
            else
                return Ok(_genericService.GetWithCondition(e => e.IsLock != true));
        }
        /// <summary>
        /// Объект.
        /// </summary>
        /// <param name="id"> Идентификатор объекта.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]    
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _genericService.GetAsync(id, typeof(TEntity).ToString());

            if (entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            return Ok(entity);
        }
        /// <summary>
        /// Создать объект.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="item"> Данные для создания объекта. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPost("{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]    
        public async Task<IActionResult> Create(int doctorId, TEntity item)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            if (await _genericService.CreateAsync(item))
                return Ok(item);//Ok("Данные успешно добавлены.");

            throw new Exception("Не предвиденная ошибка в ходе добавления новых данных.");
        }
        /// <summary>
        /// Обновить объект.
        /// </summary>
        /// <param name="id"> Идентификатор объекта.</param>
        /// <param name="item"> Данные по объекту. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]    
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
        /// <summary>
        /// Удалить объект.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="id"> Идентификатор объекта. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.HR)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]    
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