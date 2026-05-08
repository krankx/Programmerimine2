using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class ListVererohuMootmisedQueryHandler : IRequestHandler<ListVererohuMootmisedQuery, OperationResult<PagedResult<VererohuMootmine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListVererohuMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<VererohuMootmine>>> Handle(ListVererohuMootmisedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<VererohuMootmine>>();
            result.Value = await _dbContext
                .VererohuMootmised
                .OrderByDescending(m => m.Kuupaev)
                .ThenByDescending(m => m.Kellaaeg)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
