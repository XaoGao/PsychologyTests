namespace Psychology_API.Servises.ComputedHash
{
    /// <summary>
    /// Интерфейс алгоритма расчета хеша.
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// Проверка хэш пароля существующего пользователя с введеным паролем.
        /// </summary>
        /// <param name="password"> Пароль. </param>
        /// <param name="passwordHash"> Хэш пароль пользователя из БД. </param>
        /// <param name="passwordSalt"> Соль пароля из пользователя из БД. </param>
        /// <returns> Пароль и хэш пароль совпадают. </returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        /// <summary>
        /// Создание хэш(соль) пароля для нового пользователя.
        /// </summary>
        /// <param name="password"> Пароль. </param>
        /// <param name="passwordHash"> out переменная для хранения хэш пароля. </param>
        /// <param name="passwordSalt"> out переменная для хранения соли хэш пароля. </param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}