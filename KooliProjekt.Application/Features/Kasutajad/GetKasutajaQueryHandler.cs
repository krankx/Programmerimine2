using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class GetKasutajaQueryHandler : IRequestHandler<GetKasutajaQuery, OperationResult<object>>
    {
        private readonly IKasutajaRepository _kasutajaRepository;

        public GetKasutajaQueryHandler(IKasutajaRepository kasutajaRepository)
        {
            _kasutajaRepository = kasutajaRepository;
        }

        public async Task<OperationResult<object>> Handle(GetKasutajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var kasutaja = await _kasutajaRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = kasutaja.Id,
                Eesnimi = kasutaja.Eesnimi,
                Perekonnanimi = kasutaja.Perekonnanimi,
                Email = kasutaja.Email,
                Patsiendid = kasutaja.Patsiendid.Select(p => new
                {
                    p.Id,
                    p.Eesnimi,
                    p.Perekonnanimi
                })
            };

            return result;
        }
    }
}
