using AutoMapper;
using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;
using System;
using System.Linq;

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

            CreateMap<AddPatientRequest, Patient>()
                .ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(m => m.Status, o => o.MapFrom(s => PatientStatus.Screened));

            CreateMap<Patient, PatientInfoResponse>()
                .ForMember(m => m.MedicineType, o => o.MapFrom(s => s.MedicineType != null ? s.MedicineType.Name : "None"))
                .ForMember(m => m.LastVisitDate, o => o.MapFrom(s => s.Visits.OrderByDescending(v => v.Date).ToList()[0].Date));

            CreateMap<Patient, PatientResponse>()
                .ForMember(m => m.LastVisitDate, o => o.MapFrom(s => s.Visits.OrderByDescending(v => v.Date).ToList()[0].Date))
                .ForMember(m => m.Medicines, o => o.MapFrom(s => s.Visits.Where(v => v.Medicine != null)
                    .Select(v => new PatientMedicineResponse()
                    {
                        Id = v.Medicine!.Id,
                        Name = v.Medicine.Name
                    })
                    .GroupBy(x => x.Id).Select(x => x.First())));
        }
    }
}
