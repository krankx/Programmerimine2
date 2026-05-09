using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class GetToiduaineQueryHandler : IRequestHandler<GetToiduaineQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetToiduaineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetToiduaineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Toiduained
                .Where(t => t.Id == request.Id)
                .Select(t => new
                {
                    Id = t.Id,
                    Nimetus = t.Nimetus,
                    Energia = t.Energia,
                    Valgud = t.Valgud,
                    Susivesikud = t.Susivesikud,
                    MillestSuhkrud = t.MillestSuhkrud,
                    Rasvad = t.Rasvad,
                    MillestKullastunud = t.MillestKullastunud,
                    Kiudained = t.Kiudained,
                    Sool = t.Sool
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
