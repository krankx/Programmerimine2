using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class GetKasutajaQueryHandler : IRequestHandler<GetKasutajaQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetKasutajaQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetKasutajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Kasutajad
                .Include(k => k.Patsiendid)
                .Where(k => k.Id == request.Id)
                .Select(k => new
                {
                    Id = k.Id,
                    Eesnimi = k.Eesnimi,
                    Perekonnanimi = k.Perekonnanimi,
                    Email = k.Email,
                    Patsiendid = k.Patsiendid.Select(p => new
                    {
                        p.Id,
                        p.Eesnimi,
                        p.Perekonnanimi
                    })
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
