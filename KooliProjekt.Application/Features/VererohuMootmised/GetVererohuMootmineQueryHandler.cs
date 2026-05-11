using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class GetVererohuMootmineQueryHandler : IRequestHandler<GetVererohuMootmineQuery, OperationResult<VererohuMootmineDto>>
    {
        private readonly IVererohuMootmineRepository _vererohuMootmineRepository;

        public GetVererohuMootmineQueryHandler(IVererohuMootmineRepository vererohuMootmineRepository)
        {
            if (vererohuMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(vererohuMootmineRepository));
            }
            _vererohuMootmineRepository = vererohuMootmineRepository;
        }

        public async Task<OperationResult<VererohuMootmineDto>> Handle(GetVererohuMootmineQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<VererohuMootmineDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var mootmine = await _vererohuMootmineRepository.GetByIdAsync(request.Id);

            if (mootmine == null)
            {
                return result;
            }

            result.Value = new VererohuMootmineDto
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
