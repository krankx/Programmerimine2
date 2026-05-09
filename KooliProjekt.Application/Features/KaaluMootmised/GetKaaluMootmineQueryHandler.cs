using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class GetKaaluMootmineQueryHandler : IRequestHandler<GetKaaluMootmineQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetKaaluMootmineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetKaaluMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .KaaluMootmised
                .Where(m => m.Id == request.Id)
                .Select(m => new
                {
                    Id = m.Id,
                    Kuupaev = m.Kuupaev,
                    Kaal = m.Kaal,
                    PatsientId = m.PatsientId
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
