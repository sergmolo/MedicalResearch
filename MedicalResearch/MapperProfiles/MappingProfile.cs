using AutoMapper;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using System;

namespace MedicalResearch.MapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<RegisterRequest, User>()
                .ForMember(m => m.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(m => m.IsRemoved, o => o.MapFrom(s => false))
                .ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(m => m.PasswordCreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(m => m.Role, o => o.MapFrom(s => Role.Researcher));

            CreateMap<ClinicRequest, Clinic>()
                .ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow));
            CreateMap<Clinic, ClinicResponse>();
        }
    }
}
