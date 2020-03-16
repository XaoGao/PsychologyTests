using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Settings.Patients;
using Psychology_Domain.Domain;
using Psychology_XUnit.DataContextTest;
using Xunit;

namespace Psychology_XUnit.Repository
{
    public class PatientRepositoryTest
    {
        private ConnectionFactory _factory;
        private DataContext _context;
        private PatientRepository _patientRepository;
        public PatientRepositoryTest()
        {
            _factory = new ConnectionFactory();
            _context = _factory.CreateContextForInMemory();
            _patientRepository = new PatientRepository(_context);
            _patientRepository.SetInCashe += FakeSetCache;
            _patientRepository.GetFromCashe += FakeGetCache;
            _patientRepository.RemoveItemInCashe += FakeRemoveCache;
        }
        [Fact]
        public async void AddPatient_Successful_Test()
        {
            var patient = new Patient { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", DoctorId = 1 };

            var patients = await _patientRepository.GetPatientsRepositoryAsync(PatientsType.AllPatients);

            Assert.Empty(patients);

            _patientRepository.Add(patient);
            await _patientRepository.SaveAllAsync();

            patients = await _patientRepository.GetPatientsRepositoryAsync(PatientsType.AllPatients);

            Assert.Single(patients);
            Assert.Equal(1, patients.FirstOrDefault().Id);
        }
        [Fact]
        public async void RemovePatient_Successful_Test()
        {
            var patient = new Patient { Id = 1, IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", DoctorId = 1 };

            _patientRepository.Add(patient);
            await _patientRepository.SaveAllAsync();

            var patients = await _patientRepository.GetPatientsRepositoryAsync(PatientsType.AllPatients);

            Assert.NotEmpty(patients);

            _patientRepository.Remove(patient);
            await _patientRepository.SaveAllAsync();

            patients = await _patientRepository.GetPatientsRepositoryAsync(PatientsType.AllPatients);

            Assert.Empty(patients);
        }
        [Fact]
        public async void PatientIsExist_Successful_Test()
        {
            var patient = new Patient { Id = 1, PersonalCardNumber = "test-1", IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", DoctorId = 1 };

            _patientRepository.Add(patient);
            await _patientRepository.SaveAllAsync();

            var result = await _patientRepository.PatientIsExistRepositoryAsync(patient.PersonalCardNumber);

            Assert.False(result);
        }
        private async Task<bool> SetDoctorsTest()
        {
            var doctors = new List<Doctor>
            {
                new Doctor { IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", RoleId = 1, DepartmentId = 1, PositionId = 1, PhoneId = 1, Phone = new Phone(), Department = new Department(), Position = new Position() },
                new Doctor { IsLock = false, Firstname = "Петр", Lastname = "Петров", Middlename = "Петрович", RoleId = 1, DepartmentId = 1, PositionId = 1, PhoneId = 1, Phone = new Phone(), Department = new Department(), Position = new Position() },
                new Doctor { IsLock = false, Firstname = "Андрей", Lastname = "Андреевич", Middlename = "Андреев", RoleId = 2, DepartmentId = 1, PositionId = 1, PhoneId = 1, Phone = new Phone(), Department = new Department(), Position = new Position() },
                new Doctor { IsLock = true, Firstname = "Василий", Lastname = "Васильев", Middlename = "Васильевич", RoleId = 2, DepartmentId = 1, PositionId = 1, PhoneId = 1, Phone = new Phone(), Department = new Department(), Position = new Position() },
            };

            foreach (var item in doctors)
            {
                _context.Doctors.Add(item);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        private  async Task<bool> SetPatientTest()
        {
            var patients = new List<Patient>
            {
                new Patient { PersonalCardNumber = "test-1", IsLock = false, Firstname = "Иван", Lastname = "Иванов", Middlename = "Иванович", DoctorId = 1 },
                new Patient { PersonalCardNumber = "test-2", IsLock = false, Firstname = "Петр", Lastname = "Петров", Middlename = "Петрович", DoctorId = 1 },
                new Patient { PersonalCardNumber = "test-3", IsLock = false, Firstname = "Андрей", Lastname = "Андреевич", Middlename = "Андреев", DoctorId = 2 },
                new Patient { PersonalCardNumber = "test-4", IsLock = false, Firstname = "Василий", Lastname = "Васильев", Middlename = "Васильевич", DoctorId = 2 },
                new Patient { PersonalCardNumber = "test-5", IsLock = false, Firstname = "Аврора", Lastname = "Авроров", Middlename = "Аврорович", DoctorId = 3 },
                new Patient { PersonalCardNumber = "test-6", IsLock = true, Firstname = "Никита", Lastname = "Никитов", Middlename = "Никитович", DoctorId = 3 },
                new Patient { PersonalCardNumber = "test-7", IsLock = true, Firstname = "Ян", Lastname = "Янов", Middlename = "Янович", DoctorId = 4 },
                new Patient { PersonalCardNumber = "test-8", IsLock = true, Firstname = "Ксения", Lastname = "Ксенов", Middlename = "Ксенович", DoctorId = 4 },
            };

            foreach (var item in patients)
            {
                _context.Patients.Add(item);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        private async void SetData()
        {
            await SetDoctorsTest();
            await SetPatientTest();
        }
        private void FakeSetCache(string id, string suffix, Patient item)
        {
            return;
        }
        private void FakeRemoveCache(string id, string suffix)
        {
            return;
        }
        private Patient FakeGetCache(string id, string suffix)
        {
            return null;
        }
    }
}