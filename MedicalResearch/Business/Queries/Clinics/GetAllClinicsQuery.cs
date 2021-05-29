using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Clinics
{
    public class GetAllClinicsQuery : IRequest<IEnumerable<ClinicResponse>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SortCol { get; }
        public bool Asc { get; }

        public GetAllClinicsQuery(int pageIndex, int pageSize, string sortCol, bool asc)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortCol = sortCol;
            Asc = asc;
        }
    }
}
