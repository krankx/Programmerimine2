using System;
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
            if (toiduaineRepository == null)
            {
                throw new ArgumentNullException(nameof(toiduaineRepository));
            }

            _toiduaineRepository = toiduaineRepository;
        }

        public async Task<OperationResult> Handle(SaveToiduaineCommand request, CancellationToken cancellationToken)
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

            var toiduaine = new Toiduaine();
            if (request.Id != 0)
            {
                toiduaine = await _toiduaineRepository.GetByIdAsync(request.Id);
                if (toiduaine == null)
                {
                    result.AddError("Cannot find toiduaine with ID " + request.Id);
                    return result;
                }
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
