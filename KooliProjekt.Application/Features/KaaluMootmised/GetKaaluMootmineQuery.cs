using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class GetKaaluMootmineQuery : IRequest<OperationResult<KaaluMootmineDto>>
    {
        public int Id { get; set; }
    }
}
