using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class GetPatsientQueryHandler : IRequestHandler<GetPatsientQuery, OperationResult<object>>
    {
        private readonly IPatsientRepository _patsientRepository;

        public GetPatsientQueryHandler(IPatsientRepository patsientRepository)
        {
            _patsientRepository = patsientRepository;
        }

        public async Task<OperationResult<object>> Handle(GetPatsientQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var patsient = await _patsientRepository.GetByIdAsync(request.Id);

            result.Value = new
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
