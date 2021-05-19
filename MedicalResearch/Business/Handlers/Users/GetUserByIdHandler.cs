﻿using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Users;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using MedicalResearch.V1.Responses;
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
