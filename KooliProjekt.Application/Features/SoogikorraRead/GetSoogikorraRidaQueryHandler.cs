using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class GetSoogikorraRidaQueryHandler : IRequestHandler<GetSoogikorraRidaQuery, OperationResult<SoogikorraRidaDto>>
    {
        private readonly ISoogikorraRidaRepository _soogikorraRidaRepository;

        public GetSoogikorraRidaQueryHandler(ISoogikorraRidaRepository soogikorraRidaRepository)
        {
            if (soogikorraRidaRepository == null)
            {
                throw new ArgumentNullException(nameof(soogikorraRidaRepository));
            }
            _soogikorraRidaRepository = soogikorraRidaRepository;
        }

        public async Task<OperationResult<SoogikorraRidaDto>> Handle(GetSoogikorraRidaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<SoogikorraRidaDto>();

            if (request == null)
            {
                return result;
            }

            var rida = await _soogikorraRidaRepository.GetByIdAsync(request.Id);

            if (rida == null)
            {
                return result;
            }

            result.Value = new SoogikorraRidaDto
            {
                Id = rida.Id,
                Kogus = rida.Kogus,
                SoogikordId = rida.SoogikordId,
                ToiduaineId = rida.ToiduaineId
            };

            return result;
        }
    }
}
