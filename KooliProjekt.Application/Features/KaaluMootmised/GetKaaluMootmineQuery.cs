using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class GetKaaluMootmineQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
