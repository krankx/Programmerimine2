using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class ListSoogikorraReadQueryHandler : IRequestHandler<ListSoogikorraReadQuery, OperationResult<IList<SoogikorraRida>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListSoogikorraReadQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<SoogikorraRida>>> Handle(ListSoogikorraReadQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<SoogikorraRida>>();
            result.Value = await _dbContext
                .SoogikorraRead
                .OrderBy(r => r.Id)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
