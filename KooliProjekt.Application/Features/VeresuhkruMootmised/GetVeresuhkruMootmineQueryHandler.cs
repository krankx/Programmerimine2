using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class GetVeresuhkruMootmineQueryHandler : IRequestHandler<GetVeresuhkruMootmineQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetVeresuhkruMootmineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetVeresuhkruMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .VeresuhkruMootmised
                .Where(m => m.Id == request.Id)
                .Select(m => new
                {
                    Id = m.Id,
                    Kuupaev = m.Kuupaev,
                    Kellaaeg = m.Kellaaeg,
                    Veresuhkur = m.Veresuhkur,
                    PatsientId = m.PatsientId
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
