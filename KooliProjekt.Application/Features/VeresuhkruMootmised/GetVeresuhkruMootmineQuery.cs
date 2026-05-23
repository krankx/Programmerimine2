using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    [ExcludeFromCodeCoverage]
    public class GetVeresuhkruMootmineQuery : IRequest<OperationResult<VeresuhkruMootmineDto>>
    {
        public int Id { get; set; }
    }
}
