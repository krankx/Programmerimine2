using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class GetPatsientQueryHandler : IRequestHandler<GetPatsientQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetPatsientQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetPatsientQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Patsiendid
                .Where(p => p.Id == request.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Eesnimi = p.Eesnimi,
                    Perekonnanimi = p.Perekonnanimi,
                    Isikukood = p.Isikukood,
                    Synniaeg = p.Synniaeg,
                    KasutajaId = p.KasutajaId
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
