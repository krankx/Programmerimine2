using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    [ExcludeFromCodeCoverage]
    public class ListKasutajadQuery : IRequest<OperationResult<PagedResult<Kasutaja>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Email { get; set; }
    }
}
