using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class DeleteVeresuhkruMootmineCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
