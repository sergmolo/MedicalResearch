using AutoMapper;
using MedicalResearch.Enums;
using MedicalResearch.Models;
using MedicalResearch.Requests;
using MedicalResearch.Responses;

namespace MedicalResearch
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserResponse>()
				.ForMember(
					a => a.Role,
					o => o.MapFrom(s => (Role)s.RoleId)
				);
			CreateMap<RegisterRequest, User>();

			CreateMap<ClinicRequest, Clinic>();
			CreateMap<Clinic, ClinicResponse>();

			CreateMap<AddToStockRequest, Stock>();
			CreateMap<Stock, StockResponse>();

			CreateMap<SupplyRequest, Supply>();

			CreateMap<MedicineRequest, Medicine>();
			CreateMap<Medicine, MedicineResponse>()
				.ForMember(
					m => m.MedicineType,
					o => o.MapFrom(s => s.MedicineType.Name)
				)
				.ForMember(
					m => m.DosageForm,
					o => o.MapFrom(s => s.DosageForm.Name)
				)
				.ForMember(
					m => m.Container,
					o => o.MapFrom(s => s.Container.Name)
				);

			CreateMap<PatientRequest, Patient>();
			CreateMap<Patient, PatientInfoResponse>()
				.ForMember(
					m => m.Number,
					o => o.MapFrom(s => string.Format("{0:000}-{1:0000}", s.ClinicId, s.PatientNumber))
				)
				.ForMember(
					m => m.MedicineType,
					o => o.MapFrom(s => s.MedicineType != null ? s.MedicineType.Name : "None")
				);
			CreateMap<Patient, PatientResponse>()
				.ForMember(
					m => m.Number,
					o => o.MapFrom(s => string.Format("{0:000}-{1:0000}", s.ClinicId, s.PatientNumber))
				);
		}
	}
}
