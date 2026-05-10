using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class GetToiduaineQueryHandler : IRequestHandler<GetToiduaineQuery, OperationResult<object>>
    {
        private readonly IToiduaineRepository _toiduaineRepository;

        public GetToiduaineQueryHandler(IToiduaineRepository toiduaineRepository)
        {
            _toiduaineRepository = toiduaineRepository;
        }

        public async Task<OperationResult<object>> Handle(GetToiduaineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var toiduaine = await _toiduaineRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = toiduaine.Id,
                Nimetus = toiduaine.Nimetus,
                Energia = toiduaine.Energia,
                Valgud = toiduaine.Valgud,
                Susivesikud = toiduaine.Susivesikud,
                MillestSuhkrud = toiduaine.MillestSuhkrud,
                Rasvad = toiduaine.Rasvad,
                MillestKullastunud = toiduaine.MillestKullastunud,
                Kiudained = toiduaine.Kiudained,
                Sool = toiduaine.Sool
            };

            return result;
        }
    }
}
