using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class SaveKasutajaCommandHandler : IRequestHandler<SaveKasutajaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveKasutajaCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveKasutajaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var kasutaja = new Kasutaja();
            if (request.Id == 0)
            {
                await _dbContext.Kasutajad.AddAsync(kasutaja);
            }
            else
            {
                kasutaja = await _dbContext.Kasutajad.FindAsync(request.Id);
            }

            kasutaja.Eesnimi = request.Eesnimi;
            kasutaja.Perekonnanimi = request.Perekonnanimi;
            kasutaja.Email = request.Email;
            kasutaja.Parool = request.Parool;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
