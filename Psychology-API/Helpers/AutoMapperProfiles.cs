using AutoMapper;
using Psychology_API.Dtos;
using Psychology_Domain.Domain;

namespace Psychology_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DoctorForRegisterDto, Doctor>();
            CreateMap<DoctorForLoginDto, Doctor>();
        }
    }
}