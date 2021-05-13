using AutoMapper;
using MediatR;
using MedicalResearch.Models;
using MedicalResearch.Requests;
using MedicalResearch.Responses;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
	public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _dbContext;

		public GetUserByIdHandler(ApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			User user = await _dbContext.Users.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id.Equals(request.Id), cancellationToken);
			return _mapper.Map<UserResponse>(user);
		}
	}
}
