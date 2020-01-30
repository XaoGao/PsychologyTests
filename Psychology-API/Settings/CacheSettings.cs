using Microsoft.Extensions.Configuration;

namespace Psychology_API.Settings
{
    /// <summary>
    /// Настройки кеша.
    /// </summary>
    // TODO: Если изменить ключ-значение хранилища (например redis) то нужно добавить необходимые поля в appsettings поля и добавить в данный класс.
    public class CacheSettings
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Создать экземпляр класса.
        /// </summary>
        /// <param name="configuration"></param>
        public CacheSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Время жизни кеш данных в минутах.
        /// </summary>
        /// <typeparam name="int"> Время жизни кеша. </typeparam>
        /// <returns></returns>
        public int TimeLifeInMinut => _configuration.GetValue<int>("CacheSettings:TimeLifeInMinut");
        /// <summary>
        /// IP адрес до кеш хранилища.
        /// </summary>
        /// <typeparam name="string"> Расположения кеш хранилища. </typeparam>
        /// <returns></returns>
        // public string HostLocation => _configuration.GetValue<string>("CacheSettings:Host");
        /// <summary>
        /// Логин для обращения к кеш хранилищу.
        /// </summary>
        /// <typeparam name="string"> Логин. </typeparam>
        /// <returns></returns>
        // public string Login => _configuration.GetValue<string>("CacheSettings:Login");
        /// <summary>
        /// Пароль для доступка к кеш хранилищу
        /// </summary>
        /// <typeparam name="string"> Пароль. </typeparam>
        /// <returns></returns>
        // public string Password => _configuration.GetValue<string>("CacheSettings:Password");
    }
}