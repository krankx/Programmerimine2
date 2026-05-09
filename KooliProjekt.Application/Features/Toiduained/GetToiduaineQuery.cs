using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class GetToiduaineQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
