using System;
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
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _dbContext;

        public ListPatsiendidQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Patsient>>> Handle(ListPatsiendidQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Page <= 0)
            {
                throw new ArgumentException("Page must be greater than zero", nameof(request));
            }
            if (request.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than zero", nameof(request));
            }
            if (request.PageSize > MaxPageSize)
            {
                throw new ArgumentException("PageSize is too large", nameof(request));
            }

            var result = new OperationResult<PagedResult<Patsient>>();
            result.Value = await _dbContext
                .Patsiendid
                .OrderBy(p => p.Perekonnanimi)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
