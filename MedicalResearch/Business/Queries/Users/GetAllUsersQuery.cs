using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
    {

    }
}
