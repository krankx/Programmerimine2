using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class GetKasutajaQueryHandler : IRequestHandler<GetKasutajaQuery, OperationResult<KasutajaDetailsDto>>
    {
        private readonly IKasutajaRepository _kasutajaRepository;

        public GetKasutajaQueryHandler(IKasutajaRepository kasutajaRepository)
        {
            if (kasutajaRepository == null)
            {
                throw new ArgumentNullException(nameof(kasutajaRepository));
            }
            _kasutajaRepository = kasutajaRepository;
        }

        public async Task<OperationResult<KasutajaDetailsDto>> Handle(GetKasutajaQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<KasutajaDetailsDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var kasutaja = await _kasutajaRepository.GetByIdAsync(request.Id);

            if (kasutaja == null)
            {
                return result;
            }

            result.Value = new KasutajaDetailsDto
            {
                Id = kasutaja.Id,
                Eesnimi = kasutaja.Eesnimi,
                Perekonnanimi = kasutaja.Perekonnanimi,
                Email = kasutaja.Email,
                Patsiendid = kasutaja.Patsiendid.Select(p => new PatsientDto
                {
                    Id = p.Id,
                    Eesnimi = p.Eesnimi,
                    Perekonnanimi = p.Perekonnanimi,
                    Isikukood = p.Isikukood,
                    Synniaeg = p.Synniaeg,
                    KasutajaId = p.KasutajaId
                }).ToList()
            };

            return result;
        }
    }
}
