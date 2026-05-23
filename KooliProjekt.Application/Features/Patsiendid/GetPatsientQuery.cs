using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    [ExcludeFromCodeCoverage]
    public class GetPatsientQuery : IRequest<OperationResult<PatsientDto>>
    {
        public int Id { get; set; }
    }
}
