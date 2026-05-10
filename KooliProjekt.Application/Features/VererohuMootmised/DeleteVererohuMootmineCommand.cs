using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class DeleteVererohuMootmineCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
