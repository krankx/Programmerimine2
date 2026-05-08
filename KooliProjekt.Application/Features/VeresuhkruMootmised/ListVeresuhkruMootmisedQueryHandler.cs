using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class ListVeresuhkruMootmisedQueryHandler : IRequestHandler<ListVeresuhkruMootmisedQuery, OperationResult<IList<VeresuhkruMootmine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListVeresuhkruMootmisedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<VeresuhkruMootmine>>> Handle(ListVeresuhkruMootmisedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<VeresuhkruMootmine>>();
            result.Value = await _dbContext
                .VeresuhkruMootmised
                .OrderByDescending(m => m.Kuupaev)
                .ThenByDescending(m => m.Kellaaeg)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
