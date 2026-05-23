using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    [ExcludeFromCodeCoverage]
    public class GetKaaluMootmineQuery : IRequest<OperationResult<KaaluMootmineDto>>
    {
        public int Id { get; set; }
    }
}
