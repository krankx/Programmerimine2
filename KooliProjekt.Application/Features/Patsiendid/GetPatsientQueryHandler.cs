using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class GetPatsientQueryHandler : IRequestHandler<GetPatsientQuery, OperationResult<PatsientDto>>
    {
        private readonly IPatsientRepository _patsientRepository;

        public GetPatsientQueryHandler(IPatsientRepository patsientRepository)
        {
            if (patsientRepository == null)
            {
                throw new ArgumentNullException(nameof(patsientRepository));
            }
            _patsientRepository = patsientRepository;
        }

        public async Task<OperationResult<PatsientDto>> Handle(GetPatsientQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<PatsientDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var patsient = await _patsientRepository.GetByIdAsync(request.Id);

            if (patsient == null)
            {
                return result;
            }

            result.Value = new PatsientDto
            {
                Id = patsient.Id,
                Eesnimi = patsient.Eesnimi,
                Perekonnanimi = patsient.Perekonnanimi,
                Isikukood = patsient.Isikukood,
                Synniaeg = patsient.Synniaeg,
                KasutajaId = patsient.KasutajaId
            };

            return result;
        }
    }
}
