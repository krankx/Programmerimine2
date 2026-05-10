using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class SaveToiduaineCommandHandler : IRequestHandler<SaveToiduaineCommand, OperationResult>
    {
        private readonly IToiduaineRepository _toiduaineRepository;

        public SaveToiduaineCommandHandler(IToiduaineRepository toiduaineRepository)
        {
            _toiduaineRepository = toiduaineRepository;
        }

        public async Task<OperationResult> Handle(SaveToiduaineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var toiduaine = new Toiduaine();
            if (request.Id != 0)
            {
                toiduaine = await _toiduaineRepository.GetByIdAsync(request.Id);
            }

            toiduaine.Nimetus = request.Nimetus;
            toiduaine.Energia = request.Energia;
            toiduaine.Valgud = request.Valgud;
            toiduaine.Susivesikud = request.Susivesikud;
            toiduaine.MillestSuhkrud = request.MillestSuhkrud;
            toiduaine.Rasvad = request.Rasvad;
            toiduaine.MillestKullastunud = request.MillestKullastunud;
            toiduaine.Kiudained = request.Kiudained;
            toiduaine.Sool = request.Sool;

            await _toiduaineRepository.SaveAsync(toiduaine);

            return result;
        }
    }
}
