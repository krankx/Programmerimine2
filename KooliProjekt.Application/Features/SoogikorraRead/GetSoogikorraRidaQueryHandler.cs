using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class GetSoogikorraRidaQueryHandler : IRequestHandler<GetSoogikorraRidaQuery, OperationResult<object>>
    {
        private readonly ISoogikorraRidaRepository _soogikorraRidaRepository;

        public GetSoogikorraRidaQueryHandler(ISoogikorraRidaRepository soogikorraRidaRepository)
        {
            _soogikorraRidaRepository = soogikorraRidaRepository;
        }

        public async Task<OperationResult<object>> Handle(GetSoogikorraRidaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var rida = await _soogikorraRidaRepository.GetByIdAsync(request.Id);

            result.Value = new
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
