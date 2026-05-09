using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class GetVererohuMootmineQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
