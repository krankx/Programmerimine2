using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class ListSoogikorradQueryHandler : IRequestHandler<ListSoogikorradQuery, OperationResult<PagedResult<Soogikord>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListSoogikorradQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Soogikord>>> Handle(ListSoogikorradQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Soogikord>>();
            result.Value = await _dbContext
                .Soogikorrad
                .OrderByDescending(s => s.Kuupaev)
                .ThenBy(s => s.Tyyp)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
