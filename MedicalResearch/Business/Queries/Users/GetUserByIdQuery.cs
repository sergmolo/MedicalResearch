using MediatR;
using MedicalResearch.V1.Responses;

namespace MedicalResearch.Business.Queries.Users
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id { get; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
