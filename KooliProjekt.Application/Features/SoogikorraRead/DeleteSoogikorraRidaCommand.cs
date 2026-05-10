using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class DeleteSoogikorraRidaCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
