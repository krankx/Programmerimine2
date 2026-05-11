using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class GetToiduaineQueryHandler : IRequestHandler<GetToiduaineQuery, OperationResult<ToiduaineDto>>
    {
        private readonly IToiduaineRepository _toiduaineRepository;

        public GetToiduaineQueryHandler(IToiduaineRepository toiduaineRepository)
        {
            if (toiduaineRepository == null)
            {
                throw new ArgumentNullException(nameof(toiduaineRepository));
            }
            _toiduaineRepository = toiduaineRepository;
        }

        public async Task<OperationResult<ToiduaineDto>> Handle(GetToiduaineQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<ToiduaineDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            var toiduaine = await _toiduaineRepository.GetByIdAsync(request.Id);

            if (toiduaine == null)
            {
                return result;
            }

            result.Value = new ToiduaineDto
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
