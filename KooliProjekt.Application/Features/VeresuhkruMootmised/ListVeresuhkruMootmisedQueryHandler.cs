using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class ListVeresuhkruMootmisedQueryHandler : IRequestHandler<ListVeresuhkruMootmisedQuery, OperationResult<PagedResult<VeresuhkruMootmine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListVeresuhkruMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<VeresuhkruMootmine>>> Handle(ListVeresuhkruMootmisedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<VeresuhkruMootmine>>();
            result.Value = await _dbContext
                .VeresuhkruMootmised
                .OrderByDescending(m => m.Kuupaev)
                .ThenByDescending(m => m.Kellaaeg)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
