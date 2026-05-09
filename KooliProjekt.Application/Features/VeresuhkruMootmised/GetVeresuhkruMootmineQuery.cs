using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class GetVeresuhkruMootmineQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
