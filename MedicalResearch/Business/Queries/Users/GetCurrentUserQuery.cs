using MediatR;
using MedicalResearch.V1.Responses;

namespace MedicalResearch.Business.Queries.Users
{
    public class GetCurrentUserQuery : IRequest<UserResponse>
    {
    }
}
