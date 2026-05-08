using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class ListVererohuMootmisedQueryHandler : IRequestHandler<ListVererohuMootmisedQuery, OperationResult<IList<VererohuMootmine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListVererohuMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<VererohuMootmine>>> Handle(ListVererohuMootmisedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<VererohuMootmine>>();
            result.Value = await _dbContext
                .VererohuMootmised
                .OrderByDescending(m => m.Kuupaev)
                .ThenByDescending(m => m.Kellaaeg)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
