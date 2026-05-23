using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    [ExcludeFromCodeCoverage]
    public class DeleteKaaluMootmineCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
