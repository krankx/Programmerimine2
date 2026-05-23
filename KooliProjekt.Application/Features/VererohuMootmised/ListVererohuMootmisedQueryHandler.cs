using System;
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
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _dbContext;

        public ListVererohuMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<VererohuMootmine>>> Handle(ListVererohuMootmisedQuery request, CancellationToken cancellationToken)
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

            var result = new OperationResult<PagedResult<VererohuMootmine>>();

            var query = _dbContext.VererohuMootmised.AsQueryable();

            if (request.PatsientId.HasValue)
            {
                query = query.Where(m => m.PatsientId == request.PatsientId.Value);
            }

            if (request.KuupaevAlates.HasValue)
            {
                query = query.Where(m => m.Kuupaev >= request.KuupaevAlates.Value);
            }

            if (request.KuupaevKuni.HasValue)
            {
                query = query.Where(m => m.Kuupaev <= request.KuupaevKuni.Value);
            }

            result.Value = await query
                .OrderByDescending(m => m.Kuupaev)
                .ThenByDescending(m => m.Kellaaeg)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
