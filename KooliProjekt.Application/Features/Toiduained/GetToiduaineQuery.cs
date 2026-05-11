using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class GetToiduaineQuery : IRequest<OperationResult<ToiduaineDto>>
    {
        public int Id { get; set; }
    }
}
