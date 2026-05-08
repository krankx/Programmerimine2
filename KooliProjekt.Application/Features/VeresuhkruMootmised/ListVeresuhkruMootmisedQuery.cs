using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class ListVeresuhkruMootmisedQuery : IRequest<OperationResult<PagedResult<VeresuhkruMootmine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
