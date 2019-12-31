using AutoMapper;
using Psychology_API.Dtos;
using Psychology_Domain.Domain;
using System.Linq;

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

            CreateMap<Doctor, DoctorForReturnDto>()
                .IncludeAllDerived();

            // Пациент.
            CreateMap<PatientForCreateDto, Patient>();
            CreateMap<PatientForUpdateDto, Patient>();

            CreateMap<Patient, PatientForListDto>()
                .ForMember(dest => dest.Conclusion, opt => {
                    opt.MapFrom(src => src.Anamneses.FirstOrDefault(p => p.IsLast == true).Conclusion);
                });

            // Анамнез.
            CreateMap<Anamnesis, AnamnesisForReturnDto>();

            // Телефон.
            CreateMap<Department, Department>();
            CreateMap<Position, Position>();
            CreateMap<Phone, Phone>();

            // Тесты.
            CreateMap<Test, TestForReturnListDto>()
                .IncludeAllDerived();
        }
    }
}