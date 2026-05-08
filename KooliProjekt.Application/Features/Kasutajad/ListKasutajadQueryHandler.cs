using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class ListKasutajadQueryHandler : IRequestHandler<ListKasutajadQuery, OperationResult<PagedResult<Kasutaja>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListKasutajadQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Kasutaja>>> Handle(ListKasutajadQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Kasutaja>>();
            result.Value = await _dbContext
                .Kasutajad
                .OrderBy(k => k.Perekonnanimi)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
