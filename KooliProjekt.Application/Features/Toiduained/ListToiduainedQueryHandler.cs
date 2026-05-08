using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class ListToiduainedQueryHandler : IRequestHandler<ListToiduainedQuery, OperationResult<IList<Toiduaine>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListToiduainedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Toiduaine>>> Handle(ListToiduainedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Toiduaine>>();
            result.Value = await _dbContext
                .Toiduained
                .OrderBy(t => t.Nimetus)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
