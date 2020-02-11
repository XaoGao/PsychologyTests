using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsRepositoryAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();

            return doctors;
        }

        public async Task<Role> GetRoleRepositoryAsync(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);

            return role;
        }

        public async Task<IEnumerable<Role>> GetRolesRepositoryAsync()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }
    }
}