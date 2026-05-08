using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class ListPatsiendidQueryHandler : IRequestHandler<ListPatsiendidQuery, OperationResult<PagedResult<Patsient>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListPatsiendidQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Patsient>>> Handle(ListPatsiendidQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Patsient>>();
            result.Value = await _dbContext
                .Patsiendid
                .OrderBy(p => p.Perekonnanimi)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
