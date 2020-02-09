using Psychology_API.Repositories.Contracts;
using Psychology_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Psychology_Domain.Domain;
using System;

namespace Psychology_XUnit.Controllers
{
    public class PatientsControllerTest
    {
        // private readonly ILogger<PatientsController> logger;
        // private readonly IMapper mapper;
        // private readonly IPatientRepository repo;

        // public PatientsControllerTest()
        // {
        //     logger = CreateLoggerMoq().Object;
        //     mapper = CreateMapperMoq().Object;
        // }
        // [Fact]
        // public void GetAllPatients_ReturnOkResult()
        // {
        //     const int doctorId = 1;
        //     var mock = new Mock<IPatientRepository>();
        //     mock.Setup(m => m.GetPatientsAsync(doctorId)).ReturnsAsync(CreatePatientsTest());

        //     PatientsController controller = new PatientsController(mock.Object, mapper, logger);

        //     var result = controller.GetPatients(doctorId);

        //     // Assert.IsType<OkObjectResult>(result.Result);
        //     Assert.Equal(1, 1);
        // }
        // private Mock<IMapper> CreateMapperMoq()
        // {
        //     var mapperMock = new Mock<IMapper>();

        //     return mapperMock;
        // }
        // private Mock<ILogger<PatientsController>> CreateLoggerMoq()
        // {
        //     var loggerMock = new Mock<ILogger<PatientsController>>();

        //     return loggerMock;
        // }
        // private IEnumerable<Patient> CreatePatientsTest()
        // {
        //     var patients = new List<Patient>
        //     {
        //         new Patient {Id = 1, Lastname = "test", Firstname = "test", Middlename = "test", DateOfBirth = DateTime.Now, DoctorId = 1, PersonalCardNumber = "test", IsDelete = false},
        //         new Patient {Id = 2, Lastname = "test1", Firstname = "test1", Middlename = "test1", DateOfBirth = DateTime.Now, DoctorId = 1, PersonalCardNumber = "test1", IsDelete = false},
        //         new Patient {Id = 3, Lastname = "test2", Firstname = "test2", Middlename = "test2", DateOfBirth = DateTime.Now, DoctorId = 1, PersonalCardNumber = "test2", IsDelete = true},
        //         new Patient {Id = 4, Lastname = "test3", Firstname = "test3", Middlename = "test3", DateOfBirth = DateTime.Now, DoctorId = 2, PersonalCardNumber = "test3", IsDelete = false},
        //     };

        //     return patients;
        // }
    }
}