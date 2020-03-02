using AutoMapper;
using Psychology_API.Dtos;
using Psychology_Domain.Domain;
using System.Linq;
using System;
using Psychology_API.Dtos.AnamnesisDto;
using Psychology_API.Dtos.DoctorDto;
using Psychology_API.Dtos.DocumentDto;
using Psychology_API.Dtos.InterdepartDto;
using Psychology_API.Dtos.PatientDto;
using Psychology_API.Dtos.PatientTestResultDto;
using Psychology_API.Dtos.TestDto;
using Psychology_API.Dtos.VacationDto;

namespace Psychology_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Доктор.
            CreateMap<DoctorForRegisterDto, Doctor>();
            CreateMap<DoctorForLoginDto, Doctor>();
            
            CreateMap<DoctorForUpdateDto, Doctor>();

            CreateMap<Doctor, DoctorForReturnDto>();

            CreateMap<Doctor, DoctorForListReturnDto>();

            // Пациент.
            CreateMap<PatientForCreateDto, Patient>();
            CreateMap<PatientForUpdateDto, Patient>();

            CreateMap<Patient, PatientForListDto>()
                .ForMember(dest => dest.Conclusion, opt => {
                    opt.MapFrom(src => src.Anamneses.FirstOrDefault(p => p.IsLast == true).Conclusion);
                });

            // Анамнез.
            CreateMap<Anamnesis, AnamnesisForReturnDto>();
            CreateMap<AnamnesisForCreateDto, Anamnesis>()
                .ForMember(dest => dest.ConclusionTime, opt => {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.IsLast, opt => {
                    opt.MapFrom(src => true);
                });
            

            // Телефоный справочник.
            CreateMap<Department, Department>();
            CreateMap<Position, Position>();
            CreateMap<Phone, Phone>();

            // Тесты.
            CreateMap<Test, TestForReturnListDto>()
                .IncludeAllDerived();

            // Результат тестирования
            CreateMap<PatientTestResult, PatientTestResultForReturnListDto>()
                .IncludeAllDerived();
            CreateMap<PatientTestResult, PatientTestResultForReturnDetailDto>()
                .IncludeAllDerived();

            // Прием у врача
            CreateMap<Reception, ReceptionForReturnDto>()
                .ForMember(dest => dest.Fullname, opt => {
                    opt.MapFrom(src => src.Patient.Fullname);
                });
            CreateMap<ReceptionForCreateDto, Reception>();

            // Документы
            CreateMap<DocumentForCreateDto, Document>()
                .ForMember(dest => dest.DocName, opt => {
                    opt.MapFrom(src => src.File.FileName);
                });

            CreateMap<Document, DocumentForReturnListDto>()
                .IncludeAllDerived();

            // Отпуск
            CreateMap<VacationForCreateDto, Vacation>()
                .ForMember(dest => dest.CountDays, opt => {
                    opt.MapFrom(src => (src.EndVacation - src.StartVacation).Days);
                });

            // Роль
            CreateMap<Role, Role>();
            // Межведомственные запросы
            CreateMap<InterdepartRequest, InterdepartRequestDto>()
                .ForMember(dest => dest.Series, opt => {
                    opt.MapFrom(src => src.Document.Series);
                })
                .ForMember(dest => dest.Number, opt => {
                    opt.MapFrom(src => src.Document.Number);
                });;
        }
    }
}