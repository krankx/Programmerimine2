using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class ListKaaluMootmisedQuery : IRequest<OperationResult<PagedResult<KaaluMootmine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
