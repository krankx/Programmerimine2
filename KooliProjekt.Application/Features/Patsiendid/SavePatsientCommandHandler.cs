using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class SavePatsientCommandHandler : IRequestHandler<SavePatsientCommand, OperationResult>
    {
        private readonly IPatsientRepository _patsientRepository;

        public SavePatsientCommandHandler(IPatsientRepository patsientRepository)
        {
            _patsientRepository = patsientRepository;
        }

        public async Task<OperationResult> Handle(SavePatsientCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var patsient = new Patsient();
            if (request.Id != 0)
            {
                patsient = await _patsientRepository.GetByIdAsync(request.Id);
            }

            patsient.Eesnimi = request.Eesnimi;
            patsient.Perekonnanimi = request.Perekonnanimi;
            patsient.Isikukood = request.Isikukood;
            patsient.Synniaeg = request.Synniaeg;
            patsient.KasutajaId = request.KasutajaId;

            await _patsientRepository.SaveAsync(patsient);

            return result;
        }
    }
}
