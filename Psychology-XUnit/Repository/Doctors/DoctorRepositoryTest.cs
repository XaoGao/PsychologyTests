using System.Collections.Generic;
using System.Linq;
using Psychology_API.Data;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Settings;
using Psychology_API.Settings.Doctors;
using Psychology_Domain.Domain;
using Psychology_XUnit.DataContextTest;
using Xunit;

namespace Psychology_XUnit.Repository.Doctors
{
    public class DoctorRepositoryTest
    {
        private ConnectionFactory _factory;
        private DataContext _context;
        private DoctorRepository _doctorRepository;
        public DoctorRepositoryTest()
        {
            _factory = new ConnectionFactory();
            _context = _factory.CreateContextForInMemory();
            _doctorRepository = new DoctorRepository(_context);
        }
        [Fact]
        public async void AddDoctor_Successful_Test()
        {
            var doctor = new Doctor { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович" };
            _doctorRepository.Add(doctor);
            await _doctorRepository.SaveAllAsync();

            var doctors = await _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.AllDoctors);

            Assert.Single(doctors);
            Assert.Equal(1, doctors.FirstOrDefault().Id);
            Assert.Equal("Иван", doctors.FirstOrDefault().Firstname);
            Assert.False(doctors.FirstOrDefault().IsLock);
        }
        [Fact]
        public async void RemoveDoctor_Successful_Test()
        {
            var doctor = new Doctor { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович" };
            _doctorRepository.Add(doctor);
            await _doctorRepository.SaveAllAsync();

            var doctors = await _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.AllDoctors);

            Assert.Single(doctors);

            _doctorRepository.Remove(doctor);
            await _doctorRepository.SaveAllAsync();

            doctors = await _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.AllDoctors);

            Assert.Empty(doctors);
        }
        [Fact]
        public async void GetAllDoctors_Successful_Test()
        {
            SetDoctorsTest();

            var doctors = await  _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.AllDoctors);

            Assert.Equal(4, doctors.Count());
        }
        [Fact]
        public async void GetDoctorsWithRoleDoctor_Successful_Test()
        {
            SetRolesTest();
            SetDoctorsTest();

            var doctors = await _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.DoctorsWithRoleDoctor);

            Assert.Equal(2, doctors.Count());
            Assert.Contains(doctors, d => d.RoleId == 1);
        }
        [Fact]
        public async void GetEnableDoctors_Successful_Test()
        {
            SetRolesTest();
            SetDoctorsTest();

            var doctors = await _doctorRepository.GetDoctorsRepositoryAsync(DoctorsType.EnableDoctors);

            Assert.Equal(3, doctors.Count());
            Assert.Contains(doctors, d => d.IsLock == false);
        }
        [Fact]
        public async void GetDoctor_Successful_Test()
        {
            SetDoctorsTest();
            _doctorRepository.SetInCashe += FakeSetCache;
            _doctorRepository.GetFromCashe += FakeGetCache;

            var doctor = await _doctorRepository.GetDoctorRepositoryAsync(3);

            Assert.Equal(3, doctor.Id);
            Assert.False(doctor.IsLock);
            Assert.Equal("Андрей", doctor.Firstname);
        }
        private void SetDoctorsTest()
        {
            var doctors = new List<Doctor>
            {
                new Doctor() { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", RoleId = 1, DepartmentId = 1, Department = new Department(), PositionId = 1, Position = new Position(), PhoneId = 1, Phone = new Phone() },
                new Doctor() { Id = 2, IsLock = false, Firstname = "Петр", Lastname = "Петров", Middlename = "Петрович", RoleId = 1, DepartmentId = 1, Department = new Department(), PositionId = 1, Position = new Position(), PhoneId = 1, Phone = new Phone() },
                new Doctor() { Id = 3, IsLock = false, Firstname = "Андрей", Lastname = "Андреевич", Middlename = "Андреев", RoleId = 2, DepartmentId = 1, Department = new Department(), PositionId = 1, Position = new Position(), PhoneId = 1, Phone = new Phone() },
                new Doctor() { Id = 4, IsLock = true, Firstname = "Василий", Lastname = "Васильев", Middlename = "Васильевич", RoleId = 2, DepartmentId = 1, Department = new Department(), PositionId = 1, Position = new Position(), PhoneId = 1, Phone = new Phone() },
            };

            foreach (var item in doctors)
            {
                _context.Doctors.Add(item);
                _context.SaveChanges();
            }
        }
        private void SetRolesTest()
        {
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = RolesSettings.Doctor },
                new Role { Id = 2, Name = RolesSettings.HR },
            };

            foreach (var item in roles)
            {
                _context.Roles.Add(item);
                _context.SaveChanges();
            }
        }
        private void FakeSetCache(string id, string suffix, Doctor item)
        {
            return;
        }
        private void FakeRemoveCache(string id, string suffix)
        {
            return;
        }
        private Doctor FakeGetCache(string id, string suffix)
        {
            return null;
        }
    }
}