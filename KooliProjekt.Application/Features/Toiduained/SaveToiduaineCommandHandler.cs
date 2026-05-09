using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class SaveToiduaineCommandHandler : IRequestHandler<SaveToiduaineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveToiduaineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveToiduaineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var toiduaine = new Toiduaine();
            if (request.Id == 0)
            {
                await _dbContext.Toiduained.AddAsync(toiduaine);
            }
            else
            {
                toiduaine = await _dbContext.Toiduained.FindAsync(request.Id);
            }

            toiduaine.Nimetus = request.Nimetus;
            toiduaine.Energia = request.Energia;
            toiduaine.Valgud = request.Valgud;
            toiduaine.Susivesikud = request.Susivesikud;
            toiduaine.MillestSuhkrud = request.MillestSuhkrud;
            toiduaine.Rasvad = request.Rasvad;
            toiduaine.MillestKullastunud = request.MillestKullastunud;
            toiduaine.Kiudained = request.Kiudained;
            toiduaine.Sool = request.Sool;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
