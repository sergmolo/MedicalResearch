using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Medicines
{
    public class GetAllMedicinesQuery : IRequest<IEnumerable<MedicineResponse>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SortCol { get; }
        public bool Asc { get; }

        public GetAllMedicinesQuery(int pageIndex, int pageSize, string sortCol, bool asc)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortCol = sortCol;
            Asc = asc;
        }
    }
}
