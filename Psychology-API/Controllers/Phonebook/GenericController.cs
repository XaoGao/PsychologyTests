using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Repositories;
using System;
using System.Collections.Generic;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Controllers.Phonebook
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repo;
        public GenericController(IGenericRepository<TEntity> repo)
        {
            _repo = repo;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(bool param = true)
        {
            #region checkUser
            // if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized("Пользователь должен авторизоваться.");
            #endregion
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

            var entity = await _repo.GetAsync(id);

            if(entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            return Ok(entity);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int doctorId, TEntity item)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            if(await _repo.CreateAsync(item))
                return Ok("Данные успешно добавлены.");

            throw new Exception("Не предвиденная ошибка в ходе добавления новых данных.");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int doctorId, int id)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь должен авторизоваться.");

            var entity = await _repo.GetAsync(id);

            if (entity == null)
                return BadRequest("Запрашиваемых данных не существует.");

            if(await _repo.DeleteAsync(entity))
                return Ok("Данные успешно удалены.");

            throw new Exception("Запрашиваемых данных не существует.");
        }
    }
}