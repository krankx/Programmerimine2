using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class GetSoogikordQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
