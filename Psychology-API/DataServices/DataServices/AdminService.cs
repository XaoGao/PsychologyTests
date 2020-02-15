using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings.Doctors;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AdminService(DataContext context, IAdminRepository adminRepository, IDoctorRepository doctorRepository) : base(context)
        {
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
        }
        public async Task<bool> CreateRoleAsync(Role role)
        {
            return await _adminRepository.CreateRoleAsync(role);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorsType doctorsType)
        {
            return await _doctorRepository.GetDoctorsRepositoryAsync(doctorsType);
        }
        public async Task<Role> GetRoleAsync(int roleId)
        {
            return await _adminRepository.GetRoleRepositoryAsync(roleId);
        }
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _adminRepository.GetRolesRepositoryAsync();
        }
    }
}