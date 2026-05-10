using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class GetVeresuhkruMootmineQueryHandler : IRequestHandler<GetVeresuhkruMootmineQuery, OperationResult<object>>
    {
        private readonly IVeresuhkruMootmineRepository _veresuhkruMootmineRepository;

        public GetVeresuhkruMootmineQueryHandler(IVeresuhkruMootmineRepository veresuhkruMootmineRepository)
        {
            _veresuhkruMootmineRepository = veresuhkruMootmineRepository;
        }

        public async Task<OperationResult<object>> Handle(GetVeresuhkruMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var mootmine = await _veresuhkruMootmineRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = mootmine.Id,
                Kuupaev = mootmine.Kuupaev,
                Kellaaeg = mootmine.Kellaaeg,
                Veresuhkur = mootmine.Veresuhkur,
                PatsientId = mootmine.PatsientId
            };

            return result;
        }
    }
}
