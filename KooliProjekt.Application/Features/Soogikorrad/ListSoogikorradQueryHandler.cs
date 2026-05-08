using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class ListSoogikorradQueryHandler : IRequestHandler<ListSoogikorradQuery, OperationResult<IList<Soogikord>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListSoogikorradQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Soogikord>>> Handle(ListSoogikorradQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Soogikord>>();
            result.Value = await _dbContext
                .Soogikorrad
                .OrderByDescending(s => s.Kuupaev)
                .ThenBy(s => s.Tyyp)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
