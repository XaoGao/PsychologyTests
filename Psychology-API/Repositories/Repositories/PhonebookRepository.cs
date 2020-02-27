using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.ViewModels;

namespace Psychology_API.Repositories.Repositories
{
    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly DataContext _context;
        public PhonebookRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DepartmentWithDoctors>> GetPhonebookRepositoryAsync()
        {
            var departments = await _context.Departments.Where(d => d.IsLock != true).ToListAsync();
            List<DepartmentWithDoctors> phonebook = new List<DepartmentWithDoctors>();

            foreach (var department in departments)
            {
                var doctorsInDepartment = await _context.Doctors
                    .Where(d => d.DepartmentId == department.Id && d.RoleId != 1 && d.IsLock != true)
                    .Include(d => d.Position)
                    .Include(d => d.Department)
                    .Include (d => d.Phone)
                    .ToListAsync();

                if (doctorsInDepartment.Count == 0)
                    continue;

                var departmentWithWorker = new DepartmentWithDoctors(department, doctorsInDepartment);

                phonebook.Add(departmentWithWorker);
            }
            return phonebook;
        }
    }
}