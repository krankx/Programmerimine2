using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    [ExcludeFromCodeCoverage]
    public class DeleteVererohuMootmineCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
