using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class SaveSoogikordCommandHandler : IRequestHandler<SaveSoogikordCommand, OperationResult>
    {
        private readonly ISoogikordRepository _soogikordRepository;

        public SaveSoogikordCommandHandler(ISoogikordRepository soogikordRepository)
        {
            if (soogikordRepository == null)
            {
                throw new ArgumentNullException(nameof(soogikordRepository));
            }

            _soogikordRepository = soogikordRepository;
        }

        public async Task<OperationResult> Handle(SaveSoogikordCommand request, CancellationToken cancellationToken)
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

            var soogikord = new Soogikord();
            if (request.Id != 0)
            {
                soogikord = await _soogikordRepository.GetByIdAsync(request.Id);
                if (soogikord == null)
                {
                    result.AddError("Cannot find soogikord with ID " + request.Id);
                    return result;
                }
            }

            soogikord.Kuupaev = request.Kuupaev;
            soogikord.Tyyp = request.Tyyp;
            soogikord.PatsientId = request.PatsientId;

            await _soogikordRepository.SaveAsync(soogikord);

            return result;
        }
    }
}
