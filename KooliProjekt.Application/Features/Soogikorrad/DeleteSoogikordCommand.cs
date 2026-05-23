using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    [ExcludeFromCodeCoverage]
    public class DeleteSoogikordCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
