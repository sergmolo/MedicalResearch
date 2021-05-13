using MediatR;
using MedicalResearch.Responses;

namespace MedicalResearch.Requests
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
