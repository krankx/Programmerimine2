using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    [ExcludeFromCodeCoverage]
    public class DeletePatsientCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
