using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class ListSoogikorraReadQueryHandler : IRequestHandler<ListSoogikorraReadQuery, OperationResult<PagedResult<SoogikorraRida>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListSoogikorraReadQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<SoogikorraRida>>> Handle(ListSoogikorraReadQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<SoogikorraRida>>();
            result.Value = await _dbContext
                .SoogikorraRead
                .OrderBy(r => r.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
