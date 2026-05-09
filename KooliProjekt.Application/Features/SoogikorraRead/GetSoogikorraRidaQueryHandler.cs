using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class GetSoogikorraRidaQueryHandler : IRequestHandler<GetSoogikorraRidaQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetSoogikorraRidaQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetSoogikorraRidaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .SoogikorraRead
                .Where(r => r.Id == request.Id)
                .Select(r => new
                {
                    Id = r.Id,
                    Kogus = r.Kogus,
                    SoogikordId = r.SoogikordId,
                    ToiduaineId = r.ToiduaineId
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
