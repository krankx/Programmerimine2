using System;
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
            if (patsientRepository == null)
            {
                throw new ArgumentNullException(nameof(patsientRepository));
            }

            _patsientRepository = patsientRepository;
        }

        public async Task<OperationResult> Handle(SavePatsientCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var patsient = new Patsient();
            if (request.Id != 0)
            {
                patsient = await _patsientRepository.GetByIdAsync(request.Id);
                if (patsient == null)
                {
                    result.AddError("Cannot find patsient with ID " + request.Id);
                    return result;
                }
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
