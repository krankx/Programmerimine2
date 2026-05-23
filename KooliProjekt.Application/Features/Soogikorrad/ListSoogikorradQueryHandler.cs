using System;
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
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _dbContext;

        public ListSoogikorradQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Soogikord>>> Handle(ListSoogikorradQuery request, CancellationToken cancellationToken)
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

            var result = new OperationResult<PagedResult<Soogikord>>();

            var query = _dbContext.Soogikorrad.AsQueryable();

            if (request.PatsientId.HasValue)
            {
                query = query.Where(s => s.PatsientId == request.PatsientId.Value);
            }

            if (request.Tyyp.HasValue)
            {
                query = query.Where(s => s.Tyyp == request.Tyyp.Value);
            }

            if (request.KuupaevAlates.HasValue)
            {
                query = query.Where(s => s.Kuupaev >= request.KuupaevAlates.Value);
            }

            if (request.KuupaevKuni.HasValue)
            {
                query = query.Where(s => s.Kuupaev <= request.KuupaevKuni.Value);
            }

            result.Value = await query
                .OrderByDescending(s => s.Kuupaev)
                .ThenBy(s => s.Tyyp)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
