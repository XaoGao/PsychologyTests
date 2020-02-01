using AutoMapper;
using Psychology_API.Dtos;
using Psychology_Domain.Domain;
using System.Linq;
using System;

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
                // .IncludeAllDerived();

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

            // Телефон.
            CreateMap<Department, Department>();
            CreateMap<Position, Position>();
            CreateMap<Phone, Phone>();

            // Тесты.
            CreateMap<Test, TestForReturnListDto>()
                .IncludeAllDerived();
            // Прием у врача
            CreateMap<Reception, ReceptionForReturnDto>()
                .ForMember(dest => dest.Fullname, opt => {
                    opt.MapFrom(src => src.Patient.Fullname);
                });

            CreateMap<ReceptionForCreateDto, Reception>();

            // Документы
            CreateMap<DocForCreateDto, Document>()
                .ForMember(dest => dest.DocName, opt => {
                    opt.MapFrom(src => src.File.FileName);
                });

            // Отпуск
            CreateMap<VacationForCreateDto, Vacation>()
                .ForMember(dest => dest.CountDays, opt => {
                    opt.MapFrom(src => (src.EndVacation - src.StartVacation).Days);
                });
        }
    }
}