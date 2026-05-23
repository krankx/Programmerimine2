using System;
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
            if (vererohuMootmineRepository == null)
            {
                throw new ArgumentNullException(nameof(vererohuMootmineRepository));
            }

            _vererohuMootmineRepository = vererohuMootmineRepository;
        }

        public async Task<OperationResult> Handle(SaveVererohuMootmineCommand request, CancellationToken cancellationToken)
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

            var mootmine = new VererohuMootmine();
            if (request.Id != 0)
            {
                mootmine = await _vererohuMootmineRepository.GetByIdAsync(request.Id);
                if (mootmine == null)
                {
                    result.AddError("Cannot find vererohu mootmine with ID " + request.Id);
                    return result;
                }
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
