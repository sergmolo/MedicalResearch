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
                .ForMember(m => m.Role, o => o.MapFrom(s => Role.Researcher))
                .ForMember(m => m.Clinic, o => o.Ignore());

            CreateMap<AddClinicRequest, Clinic>()
                .ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow));
            CreateMap<EditClinicRequest, Clinic>()
                .ForMember(m => m.UpdatedAt, o => o.MapFrom(s => DateTime.UtcNow));
            CreateMap<Clinic, ClinicResponse>();

            CreateMap<AddMedicineRequest, Medicine>()
                .ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(m => m.MedicineType, o => o.Ignore())
                .ForMember(m => m.Container, o => o.Ignore())
                .ForMember(m => m.DosageForm, o => o.Ignore());
            CreateMap<EditMedicineRequest, Medicine>()
                .ForMember(m => m.UpdatedAt, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(m => m.MedicineType, o => o.Ignore())
                .ForMember(m => m.Container, o => o.Ignore())
                .ForMember(m => m.DosageForm, o => o.Ignore());
            CreateMap<Medicine, MedicineResponse>()
                .ForMember(m => m.MedicineType, o => o.MapFrom(s => s.MedicineType.Name))
                .ForMember(m => m.DosageForm, o => o.MapFrom(s => s.DosageForm.Name))
                .ForMember(m => m.Container, o => o.MapFrom(s => s.Container.Name));

            CreateMap<MedicineType, MedicineTypeResponse>();
            CreateMap<Container, ContainerResponse>();
            CreateMap<DosageForm, DosageFormResponse>();

            CreateMap<AddToStockRequest, Stock>();
            CreateMap<Stock, StockResponse>();
        }
    }
}
