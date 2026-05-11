using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class GetVererohuMootmineQuery : IRequest<OperationResult<VererohuMootmineDto>>
    {
        public int Id { get; set; }
    }
}
