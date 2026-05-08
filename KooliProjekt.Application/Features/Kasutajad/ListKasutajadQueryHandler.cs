using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class ListKasutajadQueryHandler : IRequestHandler<ListKasutajadQuery, OperationResult<IList<Kasutaja>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListKasutajadQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Kasutaja>>> Handle(ListKasutajadQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Kasutaja>>();
            result.Value = await _dbContext
                .Kasutajad
                .OrderBy(k => k.Perekonnanimi)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
