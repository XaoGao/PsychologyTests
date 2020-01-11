using Newtonsoft.Json;
using Psychology_API.Data;
using Psychology_API.Servises.ComputedHash;
using Psychology_Domain.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Psychology_API.SeedData
{
    public class SeedAllData
    {
        private readonly DataContext _context;
        private readonly IHash _hash;
        public SeedAllData(DataContext context, IHash hash)
        {
            _hash = hash;
            _context = context;
        }
        public void SeedData()
        {
            if (_context.Roles.Any() == true)
                return;

            //Отделы
            var departmentsFromFile = File.ReadAllText("SeedData/DataSeedDepartments.json");
            var departments = JsonConvert.DeserializeObject<List<Department>>(departmentsFromFile);

            foreach (var department in departments.OrderBy(d => d.SortLevel))
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
            }
            //Должности
            var positionsFromFile = File.ReadAllText("SeedData/DataSeedPositions.json");
            var positions = JsonConvert.DeserializeObject<List<Position>>(positionsFromFile);

            foreach (var position in positions.OrderBy(d => d.SortLevel))
            {
                _context.Positions.Add(position);
                _context.SaveChanges();
            }
            
            //Телефоны
            var phonesFromFile = File.ReadAllText("SeedData/DataSeedPhones.json");
            var phones = JsonConvert.DeserializeObject<List<Phone>>(phonesFromFile);

            foreach (var phone in phones)
            {
                _context.Phones.Add(phone);
                _context.SaveChanges();
            }
            
            //Роли
            var rolesFromFile = File.ReadAllText("SeedData/DataSeedRoles.json");
            var roles = JsonConvert.DeserializeObject<List<Role>>(rolesFromFile);

            foreach (var role in roles)
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            
            //Доктора
            var doctorsFromFile = File.ReadAllText("SeedData/DataSeedDoctors.json");
            var doctors = JsonConvert.DeserializeObject<List<Doctor>>(doctorsFromFile);

            foreach (var doctor in doctors)
            {
                byte[] passwordHash, passwordSalt;

                _hash.CreatePasswordHash("password", out passwordHash, out passwordSalt);

                doctor.PasswordHash = passwordHash;
                doctor.PasswordSalt = passwordSalt;
                doctor.Username = doctor.Username.ToLower();

                _context.Doctors.Add(doctor);
                _context.SaveChanges();
            }
            
            //Пациенты
            var patientsFromFile = File.ReadAllText("SeedData/DataSeedPatients.json");
            var patients = JsonConvert.DeserializeObject<List<Patient>>(patientsFromFile);

            foreach (var patient in patients)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            
            //Заключения
            var anamesesFromFile = File.ReadAllText("SeedData/DataSeedAnamnesis.json");
            var anameses = JsonConvert.DeserializeObject<List<Anamnesis>>(anamesesFromFile);

            foreach (var anamesis in anameses.OrderByDescending(a => a.ConclusionTime))
            {
                _context.Anamneses.Add(anamesis);
                _context.SaveChanges();
            }
            
            //Тесты
            var testsFromFile = File.ReadAllText("SeedData/DataSeedTests.json");
            var tests = JsonConvert.DeserializeObject<List<Test>>(testsFromFile);
            tests = tests.OrderByDescending(t => t.Name).ToList();

            foreach (var test in tests)
            {
                _context.Tests.Add(test);
                _context.SaveChanges();
            }
            
            //Вопросы
            var questionsFromFile = File.ReadAllText("SeedData/DataSeedQuestions.json");
            var questions = JsonConvert.DeserializeObject<List<Question>>(questionsFromFile);
            questions = questions.OrderBy(q => q.TestId).ThenBy(q => q.sortLevel).ToList();

            foreach (var question in questions)
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            
            //Ответы
            var answersFromFile = File.ReadAllText("SeedData/DataSeedAnswers.json");
            var answers = JsonConvert.DeserializeObject<List<Answer>>(answersFromFile);
            answers = answers.OrderBy(a => a.QuestionId).ThenBy(a => a.Value).ToList();

            foreach (var answer in answers)
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();
            }
            
            //Интерпритация
            var interFromFile = File.ReadAllText("SeedData/DataSeedProcessingInterpretationOfResults.json");
            var inters = JsonConvert.DeserializeObject<List<ProcessingInterpretationOfResult>>(interFromFile);

            foreach (var inter in inters)
            {
                _context.ProcessingInterpretationOfResults.Add(inter);
                _context.SaveChanges();
            }
            
            //Типы документов
            var doctypesFromFile = File.ReadAllText("SeedData/DataSeedDocumentTypes.json");
            var doctypes = JsonConvert.DeserializeObject<List<DocumentType>>(doctypesFromFile);

            foreach (var doctype in doctypes)
            {
                _context.DocumentTypes.Add(doctype);
                _context.SaveChanges();
            }
            

        }
    }
}