using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class ListKaaluMootmisedQueryHandler : IRequestHandler<ListKaaluMootmisedQuery, OperationResult<IList<KaaluMootmine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListKaaluMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<KaaluMootmine>>> Handle(ListKaaluMootmisedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<KaaluMootmine>>();
            result.Value = await _dbContext
                .KaaluMootmised
                .OrderByDescending(m => m.Kuupaev)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
