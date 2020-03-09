using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Psychology_API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    SortLevel = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterdepartStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    LevelSort = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterdepartStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    LevelLog = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    SortLevel = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    SortLevel = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Middlename = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    PhoneId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessingInterpretationOfResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    MinValue = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingInterpretationOfResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessingInterpretationOfResults_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    sortLevel = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Middlename = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PersonalCardNumber = table.Column<string>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    StartVacation = table.Column<DateTime>(nullable: false),
                    EndVacation = table.Column<DateTime>(nullable: false),
                    CountDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anamneses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    ConclusionTime = table.Column<DateTime>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false),
                    IsLast = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anamneses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anamneses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anamneses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    DocName = table.Column<string>(nullable: true),
                    Series = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    DateUpload = table.Column<DateTime>(nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    Body = table.Column<byte[]>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientTestResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    TestResultInPoints = table.Column<int>(nullable: false),
                    ProcessingInterpretationOfResultId = table.Column<int>(nullable: false),
                    DateTimeCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTestResult_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTestResult_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTestResult_ProcessingInterpretationOfResults_Processi~",
                        column: x => x.ProcessingInterpretationOfResultId,
                        principalTable: "ProcessingInterpretationOfResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTestResult_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    DateTimeReception = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterdepartRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    Request = table.Column<DateTime>(nullable: false),
                    Response = table.Column<DateTime>(nullable: false),
                    InterdepartStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterdepartRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterdepartRequests_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterdepartRequests_InterdepartStatuses_InterdepartStatusId",
                        column: x => x.InterdepartStatusId,
                        principalTable: "InterdepartStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    AnswersValue = table.Column<int>(nullable: false),
                    PatientTestResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_PatientTestResult_PatientTestResultId",
                        column: x => x.PatientTestResultId,
                        principalTable: "PatientTestResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anamneses_DoctorId",
                table: "Anamneses",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamneses_PatientId",
                table: "Anamneses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestId",
                table: "Answers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PhoneId",
                table: "Doctors",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PositionId",
                table: "Doctors",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoleId",
                table: "Doctors",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PatientId",
                table: "Documents",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_InterdepartRequests_DocumentId",
                table: "InterdepartRequests",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InterdepartRequests_InterdepartStatusId",
                table: "InterdepartRequests",
                column: "InterdepartStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTestResult_DoctorId",
                table: "PatientTestResult",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTestResult_PatientId",
                table: "PatientTestResult",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTestResult_ProcessingInterpretationOfResultId",
                table: "PatientTestResult",
                column: "ProcessingInterpretationOfResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTestResult_TestId",
                table: "PatientTestResult",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingInterpretationOfResults_TestId",
                table: "ProcessingInterpretationOfResults",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_PatientId",
                table: "QuestionsAnswers",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_PatientTestResultId",
                table: "QuestionsAnswers",
                column: "PatientTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_QuestionId",
                table: "QuestionsAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_TestId",
                table: "QuestionsAnswers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_DoctorId",
                table: "Receptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_PatientId",
                table: "Receptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_DoctorId",
                table: "Vacations",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anamneses");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "InterdepartRequests");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "InterdepartStatuses");

            migrationBuilder.DropTable(
                name: "PatientTestResult");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ProcessingInterpretationOfResults");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
