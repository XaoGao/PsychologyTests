using AutoMapper;
using Psychology_API.Dtos;
using Psychology_Domain.Domain;

namespace Psychology_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Доктор.
            CreateMap<DoctorForRegisterDto, Doctor>();
            CreateMap<DoctorForLoginDto, Doctor>();

            // Пациент.
            CreateMap<PatientForCreateDto, Patient>();
            CreateMap<PatientForUpdateDto, Patient>();
        }
    }
}