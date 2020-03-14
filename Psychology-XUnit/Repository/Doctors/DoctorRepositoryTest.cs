using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Repositories;
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

            Assert.Equal(1, doctors.Count());
            Assert.Equal(1, doctors.FirstOrDefault().Id);
            Assert.Equal(false, doctors.FirstOrDefault().IsLock);
        }
        private IEnumerable<Doctor> GetDoctorsTest()
        {
            var doctors = new List<Doctor>
            {
                new Doctor() { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович" },
                new Doctor() { Id = 2, IsLock = false, Firstname = "Петр", Lastname = "Петров", Middlename = "Петрович" },
                new Doctor() { Id = 3, IsLock = false, Firstname = "Андрей", Lastname = "Андреевич", Middlename = "Андреев" },
                new Doctor() { Id = 4, IsLock = true, Firstname = "Василий", Lastname = "Васильев", Middlename = "Васильевич" },
            };

            return doctors;
        }
    }
}