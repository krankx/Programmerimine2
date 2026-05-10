using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class SaveKasutajaCommandHandler : IRequestHandler<SaveKasutajaCommand, OperationResult>
    {
        private readonly IKasutajaRepository _kasutajaRepository;

        public SaveKasutajaCommandHandler(IKasutajaRepository kasutajaRepository)
        {
            _kasutajaRepository = kasutajaRepository;
        }

        public async Task<OperationResult> Handle(SaveKasutajaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var kasutaja = new Kasutaja();
            if (request.Id != 0)
            {
                kasutaja = await _kasutajaRepository.GetByIdAsync(request.Id);
            }

            kasutaja.Eesnimi = request.Eesnimi;
            kasutaja.Perekonnanimi = request.Perekonnanimi;
            kasutaja.Email = request.Email;
            kasutaja.Parool = request.Parool;

            await _kasutajaRepository.SaveAsync(kasutaja);

            return result;
        }
    }
}
