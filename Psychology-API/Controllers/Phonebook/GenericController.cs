using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_Domain.Abstarct;
using Psychology_API.Settings;
using AutoMapper;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repo;
        private readonly IMapper _mapper;

        public GenericController(IGenericRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(bool param = true)
        {
            #region checkUser
            // if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized("Пользователь должен авторизоваться.");
            #endregion
            //param - указатель, получить все значения или только актуальные
            if(param)
                return Ok(await _repo.GetAllAsync());
            else
                return Ok(_repo.GetWithCondition(e => e.IsLock != true));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            #region checkUser
            // if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized("Пользователь должен авторизоваться.");
            #endregion

            var entity = await _repo.GetAsync(id, typeof(TEntity).ToString());

            if(entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            return Ok(entity);
        }
        [Authorize]
        [HttpPost("{doctorId}")]
        public async Task<IActionResult> Create(int doctorId, TEntity item)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            if(await _repo.CreateAsync(item))
                return Ok("Данные успешно добавлены.");

            throw new Exception("Не предвиденная ошибка в ходе добавления новых данных.");
        }
        #region  GenericUpdate
        // [Authorize(Roles = RolesSettings.HR)]
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, TEntity item)
        // {
        //     var itemFromRepo = await _repo.GetAsync(id, typeof(TEntity).ToString());

        //     if(itemFromRepo == null)
        //         return BadRequest($"Данного объекта для обновленя нет");

        //     _mapper.Map(item, itemFromRepo);

        //     if(await _repo.UpdateAsync(item))
        //         return Ok(itemFromRepo);
            
        //     throw new Exception("Непредвиденая ошибка в ходе обновления данных");
        // }
        #endregion

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            var entity = await _repo.GetAsync(id,typeof(TEntity).ToString());

            if (entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            if(await _repo.DeleteAsync(entity))
                return Ok("Данные успешно удалены.");

            throw new Exception("Запрашиваемых данных не существует.");
        }
    }
}