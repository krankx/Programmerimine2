using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class SavePatsientCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Isikukood { get; set; }
        public DateTime Synniaeg { get; set; }
        public int KasutajaId { get; set; }
    }
}
