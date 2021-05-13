using AutoMapper;
using MediatR;
using MedicalResearch.Queries;
using MedicalResearch.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Handlers.Users
{
	public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllUsersHandler(ApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			var list = await _dbContext.Users.AsNoTracking()
				.Select(p => _mapper.Map<UserResponse>(p))
				.ToListAsync(cancellationToken);

			return list;
		}
	}
}
