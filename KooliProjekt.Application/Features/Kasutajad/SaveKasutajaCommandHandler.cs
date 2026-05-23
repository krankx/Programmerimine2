using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class SaveKasutajaCommandHandler : IRequestHandler<SaveKasutajaCommand, OperationResult>
    {
        private readonly IKasutajaRepository _kasutajaRepository;

        public SaveKasutajaCommandHandler(IKasutajaRepository kasutajaRepository)
        {
            if (kasutajaRepository == null)
            {
                throw new ArgumentNullException(nameof(kasutajaRepository));
            }

            _kasutajaRepository = kasutajaRepository;
        }

        public async Task<OperationResult> Handle(SaveKasutajaCommand request, CancellationToken cancellationToken)
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

            var kasutaja = new Kasutaja();
            if (request.Id != 0)
            {
                kasutaja = await _kasutajaRepository.GetByIdAsync(request.Id);
                if (kasutaja == null)
                {
                    result.AddError("Cannot find kasutaja with ID " + request.Id);
                    return result;
                }
            }

            kasutaja.Eesnimi = request.Eesnimi;
            kasutaja.Perekonnanimi = request.Perekonnanimi;
            kasutaja.Email = request.Email;
            kasutaja.Parool = request.Parool;

            await _kasutajaRepository.SaveAsync(kasutaja);

            return result;
        }
    }
}
