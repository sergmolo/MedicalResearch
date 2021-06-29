using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Patients
{
    public class GetAllPatientsQuery : IRequest<IEnumerable<PatientResponse>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int? ClinicId { get; }

        public GetAllPatientsQuery(int pageIndex, int pageSize, int? clinicId)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            ClinicId = clinicId;
        }
    }
}
