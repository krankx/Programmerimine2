using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class GetVererohuMootmineQueryHandler : IRequestHandler<GetVererohuMootmineQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetVererohuMootmineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetVererohuMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .VererohuMootmised
                .Where(m => m.Id == request.Id)
                .Select(m => new
                {
                    Id = m.Id,
                    Kuupaev = m.Kuupaev,
                    Kellaaeg = m.Kellaaeg,
                    Sustoolne = m.Sustoolne,
                    Diastoolne = m.Diastoolne,
                    Pulss = m.Pulss,
                    PatsientId = m.PatsientId
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
