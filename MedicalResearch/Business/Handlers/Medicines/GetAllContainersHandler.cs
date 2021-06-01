using AutoMapper;
using MediatR;
using MedicalResearch.Business.Queries.Medicines;
using MedicalResearch.Data;
using MedicalResearch.Extensions;
using MedicalResearch.V1.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Medicines
{
    public class GetAllContainersHandler : IRequestHandler<GetAllContainersQuery, IEnumerable<ContainerResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllContainersHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContainerResponse>> Handle(GetAllContainersQuery request, CancellationToken ct)
        {
            var query = _dbContext.Containers.AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(m => _mapper.Map<ContainerResponse>(m));

            return await query.ToListAsync(ct);
        }
    }
}
