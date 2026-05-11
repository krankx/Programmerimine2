using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class GetSoogikordQueryHandler : IRequestHandler<GetSoogikordQuery, OperationResult<SoogikordDetailsDto>>
    {
        private readonly ISoogikordRepository _soogikordRepository;

        public GetSoogikordQueryHandler(ISoogikordRepository soogikordRepository)
        {
            if (soogikordRepository == null)
            {
                throw new ArgumentNullException(nameof(soogikordRepository));
            }
            _soogikordRepository = soogikordRepository;
        }

        public async Task<OperationResult<SoogikordDetailsDto>> Handle(GetSoogikordQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<SoogikordDetailsDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var soogikord = await _soogikordRepository.GetByIdAsync(request.Id);

            if (soogikord == null)
            {
                return result;
            }

            result.Value = new SoogikordDetailsDto
            {
                Id = soogikord.Id,
                Kuupaev = soogikord.Kuupaev,
                Tyyp = soogikord.Tyyp,
                PatsientId = soogikord.PatsientId,
                Read = soogikord.Read.Select(r => new SoogikorraRidaDto
                {
                    Id = r.Id,
                    Kogus = r.Kogus,
                    SoogikordId = r.SoogikordId,
                    ToiduaineId = r.ToiduaineId
                }).ToList()
            };

            return result;
        }
    }
}
