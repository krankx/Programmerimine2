using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class GetKaaluMootmineQueryHandler : IRequestHandler<GetKaaluMootmineQuery, OperationResult<object>>
    {
        private readonly IKaaluMootmineRepository _kaaluMootmineRepository;

        public GetKaaluMootmineQueryHandler(IKaaluMootmineRepository kaaluMootmineRepository)
        {
            _kaaluMootmineRepository = kaaluMootmineRepository;
        }

        public async Task<OperationResult<object>> Handle(GetKaaluMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var mootmine = await _kaaluMootmineRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = mootmine.Id,
                Kuupaev = mootmine.Kuupaev,
                Kaal = mootmine.Kaal,
                PatsientId = mootmine.PatsientId
            };

            return result;
        }
    }
}
