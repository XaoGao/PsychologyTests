using Psychology_API.Data;

namespace Psychology_API.Settings
{
    /// <summary>
    /// Представление ролей в виде C# класса
    /*
        TODO: нужно продумать как реализовать данный класс, так как нас не устраивает сравние с чистой строкой плохая практика.
    */
    /// </summary>
    public static class RolesSettings
    {
        public const string Administrator = "admin";
        public const string HR = "hr";
        public const string Doctor = "doctor";
        public const string Registry = "registry";

    }
}