using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class SavePatsientCommandHandler : IRequestHandler<SavePatsientCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SavePatsientCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SavePatsientCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var patsient = new Patsient();
            if (request.Id == 0)
            {
                await _dbContext.Patsiendid.AddAsync(patsient);
            }
            else
            {
                patsient = await _dbContext.Patsiendid.FindAsync(request.Id);
            }

            patsient.Eesnimi = request.Eesnimi;
            patsient.Perekonnanimi = request.Perekonnanimi;
            patsient.Isikukood = request.Isikukood;
            patsient.Synniaeg = request.Synniaeg;
            patsient.KasutajaId = request.KasutajaId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
