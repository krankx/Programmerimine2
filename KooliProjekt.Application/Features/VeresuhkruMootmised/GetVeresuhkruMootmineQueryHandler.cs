using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class GetVeresuhkruMootmineQueryHandler : IRequestHandler<GetVeresuhkruMootmineQuery, OperationResult<VeresuhkruMootmineDto>>
    {
        private readonly IVeresuhkruMootmineRepository _veresuhkruMootmineRepository;

        public GetVeresuhkruMootmineQueryHandler(IVeresuhkruMootmineRepository veresuhkruMootmineRepository)
        {
            if (veresuhkruMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(veresuhkruMootmineRepository));
            }
            _veresuhkruMootmineRepository = veresuhkruMootmineRepository;
        }

        public async Task<OperationResult<VeresuhkruMootmineDto>> Handle(GetVeresuhkruMootmineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<VeresuhkruMootmineDto>();

            if (request == null)
            {
                return result;
            }

            var mootmine = await _veresuhkruMootmineRepository.GetByIdAsync(request.Id);

            if (mootmine == null)
            {
                return result;
            }

            result.Value = new VeresuhkruMootmineDto
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
