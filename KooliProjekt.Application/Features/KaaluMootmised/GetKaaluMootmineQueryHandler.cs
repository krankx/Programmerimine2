using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class GetKaaluMootmineQueryHandler : IRequestHandler<GetKaaluMootmineQuery, OperationResult<KaaluMootmineDto>>
    {
        private readonly IKaaluMootmineRepository _kaaluMootmineRepository;

        public GetKaaluMootmineQueryHandler(IKaaluMootmineRepository kaaluMootmineRepository)
        {
            if (kaaluMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(kaaluMootmineRepository));
            }
            _kaaluMootmineRepository = kaaluMootmineRepository;
        }

        public async Task<OperationResult<KaaluMootmineDto>> Handle(GetKaaluMootmineQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<KaaluMootmineDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var mootmine = await _kaaluMootmineRepository.GetByIdAsync(request.Id);

            if (mootmine == null)
            {
                return result;
            }

            result.Value = new KaaluMootmineDto
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
