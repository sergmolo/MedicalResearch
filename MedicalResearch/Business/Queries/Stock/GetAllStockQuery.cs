using MediatR;
using MedicalResearch.V1.Responses;
using System.Collections.Generic;

namespace MedicalResearch.Business.Queries.Stock
{
    public class GetAllStockQuery : IRequest<IEnumerable<StockResponse>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetAllStockQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
