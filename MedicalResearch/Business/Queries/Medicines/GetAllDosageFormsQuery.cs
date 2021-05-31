using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Medicines
{
    public class GetAllDosageFormsQuery : IRequest<IEnumerable<DosageFormResponse>>
    {
    }
}
