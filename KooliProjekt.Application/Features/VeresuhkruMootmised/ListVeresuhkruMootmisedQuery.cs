using System;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    [ExcludeFromCodeCoverage]
    public class ListVeresuhkruMootmisedQuery : IRequest<OperationResult<PagedResult<VeresuhkruMootmine>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int? PatsientId { get; set; }
        public DateTime? KuupaevAlates { get; set; }
        public DateTime? KuupaevKuni { get; set; }
    }
}
