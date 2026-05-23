using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    [ExcludeFromCodeCoverage]
    public class ListPatsiendidQuery : IRequest<OperationResult<PagedResult<Patsient>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Isikukood { get; set; }
        public int? KasutajaId { get; set; }
    }
}
