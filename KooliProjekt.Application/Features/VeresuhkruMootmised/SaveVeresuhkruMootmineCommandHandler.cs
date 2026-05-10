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
            _veresuhkruMootmineRepository = veresuhkruMootmineRepository;
        }

        public async Task<OperationResult> Handle(SaveVeresuhkruMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var mootmine = new VeresuhkruMootmine();
            if (request.Id != 0)
            {
                mootmine = await _veresuhkruMootmineRepository.GetByIdAsync(request.Id);
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
