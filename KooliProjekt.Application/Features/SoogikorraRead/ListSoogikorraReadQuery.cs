using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    [ExcludeFromCodeCoverage]
    public class ListSoogikorraReadQuery : IRequest<OperationResult<PagedResult<SoogikorraRida>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int? SoogikordId { get; set; }
        public int? ToiduaineId { get; set; }
    }
}
