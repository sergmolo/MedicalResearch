using MediatR;
using MedicalResearch.V1.Requests;
using MedicalResearch.V1.Responses;

namespace MedicalResearch.Business.Queries.Patients
{
    public class GetPatientQuery : IRequest<PatientInfoResponse>
    {
        public PatientRequest PatientRequest { get; }

        public GetPatientQuery(PatientRequest patientRequest)
        {
            PatientRequest = patientRequest;
        }
    }
}
