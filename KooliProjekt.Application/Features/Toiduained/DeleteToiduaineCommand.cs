using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class DeleteToiduaineCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
