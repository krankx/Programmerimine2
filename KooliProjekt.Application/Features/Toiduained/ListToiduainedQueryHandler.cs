using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class ListToiduainedQueryHandler : IRequestHandler<ListToiduainedQuery, OperationResult<PagedResult<Toiduaine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListToiduainedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Toiduaine>>> Handle(ListToiduainedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Toiduaine>>();
            result.Value = await _dbContext
                .Toiduained
                .OrderBy(t => t.Nimetus)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
