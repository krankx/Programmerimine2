using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class SaveVeresuhkruMootmineCommandHandler : IRequestHandler<SaveVeresuhkruMootmineCommand, OperationResult>
    {
        private readonly IVeresuhkruMootmineRepository _veresuhkruMootmineRepository;

        public SaveVeresuhkruMootmineCommandHandler(IVeresuhkruMootmineRepository veresuhkruMootmineRepository)
        {
            if (veresuhkruMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(veresuhkruMootmineRepository));
            }

            _veresuhkruMootmineRepository = veresuhkruMootmineRepository;
        }

        public async Task<OperationResult> Handle(SaveVeresuhkruMootmineCommand request, CancellationToken cancellationToken)
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

            var mootmine = new VeresuhkruMootmine();
            if (request.Id != 0)
            {
                mootmine = await _veresuhkruMootmineRepository.GetByIdAsync(request.Id);
                if (mootmine == null)
                {
                    result.AddError("Cannot find veresuhkru mootmine with ID " + request.Id);
                    return result;
                }
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kellaaeg = request.Kellaaeg;
            mootmine.Veresuhkur = request.Veresuhkur;
            mootmine.PatsientId = request.PatsientId;

            await _veresuhkruMootmineRepository.SaveAsync(mootmine);

            return result;
        }
    }
}
