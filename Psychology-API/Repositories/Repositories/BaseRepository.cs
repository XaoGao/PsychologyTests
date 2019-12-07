using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Repositories.Repositories
{
    /// <summary>
    /// Базовый репозиторий для работы с сущностями БД.
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="context"></param>

        public BaseRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Добавление сущности в контекст данных.
        /// </summary>
        /// <typeparam name="T"> Обобщенная сущность из контекста. </typeparam>
        public virtual void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        /// <summary>
        /// Удаление сущности из контекста данных.
        /// </summary>
        /// <typeparam name="T"> Обобщеная сущность из контекста данных. </typeparam>
        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        /// <summary>
        /// Сохранение изменений в контексте.
        /// </summary>
        /// <returns> Успешное сохранение 1 и более изменений. </returns>
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}