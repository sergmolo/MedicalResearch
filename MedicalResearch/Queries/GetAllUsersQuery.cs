using MediatR;
using MedicalResearch.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Queries
{
	public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
	{

	}
}
