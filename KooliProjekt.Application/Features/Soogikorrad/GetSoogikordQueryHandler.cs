using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class GetSoogikordQueryHandler : IRequestHandler<GetSoogikordQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetSoogikordQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetSoogikordQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Soogikorrad
                .Include(s => s.Read)
                .Where(s => s.Id == request.Id)
                .Select(s => new
                {
                    Id = s.Id,
                    Kuupaev = s.Kuupaev,
                    Tyyp = s.Tyyp,
                    PatsientId = s.PatsientId,
                    Read = s.Read.Select(r => new
                    {
                        r.Id,
                        r.Kogus,
                        r.ToiduaineId
                    })
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
