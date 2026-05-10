using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class GetVererohuMootmineQueryHandler : IRequestHandler<GetVererohuMootmineQuery, OperationResult<object>>
    {
        private readonly IVererohuMootmineRepository _vererohuMootmineRepository;

        public GetVererohuMootmineQueryHandler(IVererohuMootmineRepository vererohuMootmineRepository)
        {
            _vererohuMootmineRepository = vererohuMootmineRepository;
        }

        public async Task<OperationResult<object>> Handle(GetVererohuMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var mootmine = await _vererohuMootmineRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = mootmine.Id,
                Kuupaev = mootmine.Kuupaev,
                Kellaaeg = mootmine.Kellaaeg,
                Sustoolne = mootmine.Sustoolne,
                Diastoolne = mootmine.Diastoolne,
                Pulss = mootmine.Pulss,
                PatsientId = mootmine.PatsientId
            };

            return result;
        }
    }
}
