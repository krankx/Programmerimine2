using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class GetSoogikordQueryHandler : IRequestHandler<GetSoogikordQuery, OperationResult<object>>
    {
        private readonly ISoogikordRepository _soogikordRepository;

        public GetSoogikordQueryHandler(ISoogikordRepository soogikordRepository)
        {
            _soogikordRepository = soogikordRepository;
        }

        public async Task<OperationResult<object>> Handle(GetSoogikordQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var soogikord = await _soogikordRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = soogikord.Id,
                Kuupaev = soogikord.Kuupaev,
                Tyyp = soogikord.Tyyp,
                PatsientId = soogikord.PatsientId,
                Read = soogikord.Read.Select(r => new
                {
                    r.Id,
                    r.Kogus,
                    r.ToiduaineId
                })
            };

            return result;
        }
    }
}
