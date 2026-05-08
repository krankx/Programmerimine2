using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class ListToiduainedQuery : IRequest<OperationResult<PagedResult<Toiduaine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
