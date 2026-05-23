using System;
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
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _dbContext;

        public ListToiduainedQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Toiduaine>>> Handle(ListToiduainedQuery request, CancellationToken cancellationToken)
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

            var result = new OperationResult<PagedResult<Toiduaine>>();

            var query = _dbContext.Toiduained.AsQueryable();

            if (!string.IsNullOrEmpty(request.Nimetus))
            {
                query = query.Where(t => t.Nimetus.Contains(request.Nimetus));
            }

            if (request.EnergiaMin.HasValue)
            {
                query = query.Where(t => t.Energia >= request.EnergiaMin.Value);
            }

            if (request.EnergiaMax.HasValue)
            {
                query = query.Where(t => t.Energia <= request.EnergiaMax.Value);
            }

            result.Value = await query
                .OrderBy(t => t.Nimetus)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
