using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    [ExcludeFromCodeCoverage]
    public class GetSoogikorraRidaQuery : IRequest<OperationResult<SoogikorraRidaDto>>
    {
        public int Id { get; set; }
    }
}
