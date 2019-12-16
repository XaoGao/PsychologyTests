using Psychology_API.Data;

namespace Psychology_API.Settings
{
    /// <summary>
    /// Роли беруться их базы
    /// </summary>
    public class RolesSettings
    {
        private readonly DataContext _context;
        public RolesSettings(DataContext context)
        {
            _context = context;
        }


    }
}