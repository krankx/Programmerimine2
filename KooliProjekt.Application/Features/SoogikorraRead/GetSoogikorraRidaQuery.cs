using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class GetSoogikorraRidaQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
