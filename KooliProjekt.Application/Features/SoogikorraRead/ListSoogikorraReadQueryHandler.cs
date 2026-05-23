using System;
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
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _dbContext;

        public ListSoogikorraReadQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<SoogikorraRida>>> Handle(ListSoogikorraReadQuery request, CancellationToken cancellationToken)
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

            var result = new OperationResult<PagedResult<SoogikorraRida>>();

            var query = _dbContext.SoogikorraRead.AsQueryable();

            if (request.SoogikordId.HasValue)
            {
                query = query.Where(r => r.SoogikordId == request.SoogikordId.Value);
            }

            if (request.ToiduaineId.HasValue)
            {
                query = query.Where(r => r.ToiduaineId == request.ToiduaineId.Value);
            }

            result.Value = await query
                .OrderBy(r => r.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
