using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class SaveVererohuMootmineCommandHandler : IRequestHandler<SaveVererohuMootmineCommand, OperationResult>
    {
        private readonly IVererohuMootmineRepository _vererohuMootmineRepository;

        public SaveVererohuMootmineCommandHandler(IVererohuMootmineRepository vererohuMootmineRepository)
        {
            _vererohuMootmineRepository = vererohuMootmineRepository;
        }

        public async Task<OperationResult> Handle(SaveVererohuMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var mootmine = new VererohuMootmine();
            if (request.Id != 0)
            {
                mootmine = await _vererohuMootmineRepository.GetByIdAsync(request.Id);
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kellaaeg = request.Kellaaeg;
            mootmine.Sustoolne = request.Sustoolne;
            mootmine.Diastoolne = request.Diastoolne;
            mootmine.Pulss = request.Pulss;
            mootmine.PatsientId = request.PatsientId;

            await _vererohuMootmineRepository.SaveAsync(mootmine);

            return result;
        }
    }
}
