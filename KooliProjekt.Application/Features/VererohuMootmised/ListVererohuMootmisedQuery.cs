using System;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    [ExcludeFromCodeCoverage]
    public class ListVererohuMootmisedQuery : IRequest<OperationResult<PagedResult<VererohuMootmine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int? PatsientId { get; set; }
        public DateTime? KuupaevAlates { get; set; }
        public DateTime? KuupaevKuni { get; set; }
    }
}
