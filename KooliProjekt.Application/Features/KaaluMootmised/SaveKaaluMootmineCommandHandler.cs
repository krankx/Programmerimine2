using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class SaveKaaluMootmineCommandHandler : IRequestHandler<SaveKaaluMootmineCommand, OperationResult>
    {
        private readonly IKaaluMootmineRepository _kaaluMootmineRepository;

        public SaveKaaluMootmineCommandHandler(IKaaluMootmineRepository kaaluMootmineRepository)
        {
            if (kaaluMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(kaaluMootmineRepository));
            }

            _kaaluMootmineRepository = kaaluMootmineRepository;
        }

        public async Task<OperationResult> Handle(SaveKaaluMootmineCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var mootmine = new KaaluMootmine();
            if (request.Id != 0)
            {
                mootmine = await _kaaluMootmineRepository.GetByIdAsync(request.Id);
                if (mootmine == null)
                {
                    result.AddError("Cannot find kaalu mootmine with ID " + request.Id);
                    return result;
                }
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kaal = request.Kaal;
            mootmine.PatsientId = request.PatsientId;

            await _kaaluMootmineRepository.SaveAsync(mootmine);

            return result;
        }
    }
}
