using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class ListPatsiendidQueryHandler : IRequestHandler<ListPatsiendidQuery, OperationResult<IList<Patsient>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListPatsiendidQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Patsient>>> Handle(ListPatsiendidQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Patsient>>();
            result.Value = await _dbContext
                .Patsiendid
                .OrderBy(p => p.Perekonnanimi)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
