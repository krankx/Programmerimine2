using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class GetKasutajaQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
