using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class SaveSoogikorraRidaCommandHandler : IRequestHandler<SaveSoogikorraRidaCommand, OperationResult>
    {
        private readonly ISoogikorraRidaRepository _soogikorraRidaRepository;

        public SaveSoogikorraRidaCommandHandler(ISoogikorraRidaRepository soogikorraRidaRepository)
        {
            _soogikorraRidaRepository = soogikorraRidaRepository;
        }

        public async Task<OperationResult> Handle(SaveSoogikorraRidaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var rida = new SoogikorraRida();
            if (request.Id != 0)
            {
                rida = await _soogikorraRidaRepository.GetByIdAsync(request.Id);
            }

            rida.Kogus = request.Kogus;
            rida.SoogikordId = request.SoogikordId;
            rida.ToiduaineId = request.ToiduaineId;

            await _soogikorraRidaRepository.SaveAsync(rida);

            return result;
        }
    }
}
