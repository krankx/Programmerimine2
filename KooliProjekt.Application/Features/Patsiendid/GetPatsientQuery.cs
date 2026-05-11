using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class GetPatsientQuery : IRequest<OperationResult<PatsientDto>>
    {
        public int Id { get; set; }
    }
}
